﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BlockChainDemo.Models
{
    public class BlockChain
    {
        private readonly int _proofOfWorkDifficulty;
        private readonly double _miningReward;
        private List<Transactions> _pendingTransactions;
        public List<Block> Chain { get; set; }
        public BlockChain(int proofOfWorkDifficulty, int miningReward)
        {
            _proofOfWorkDifficulty = proofOfWorkDifficulty;
            _miningReward = miningReward;
            _pendingTransactions = new List<Transactions>();
            Chain = new List<Block> { CreateGenesisBlock() };
        }
        public void CreateTransaction(Transactions transaction)
        {
            _pendingTransactions.Add(transaction);
        }
        public void MineBlock(string minerAddress)
        {
            Transactions minerRewardTransaction = new Transactions(null, minerAddress, _miningReward);
            _pendingTransactions.Add(minerRewardTransaction);
            Block block = new Block(DateTime.Now, _pendingTransactions);
            block.MineBlock(_proofOfWorkDifficulty);
            block.PreviousHash = Chain.Last().Hash;
            Chain.Add(block);
            _pendingTransactions = new List<Transactions>();
        }
        public bool IsValidChain()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block previousBlock = Chain[i - 1];
                Block currentBlock = Chain[i];
                if (currentBlock.Hash != currentBlock.CreateHash())
                    return false;
                if (currentBlock.PreviousHash != previousBlock.Hash)
                    return false;
            }
            return true;
        }
        public double GetBalance(string address)
        {
            double balance = 0;
            foreach (Block block in Chain)
            {
                foreach (Transactions transaction in block.transactions)
                {
                    if (transaction.From == address)
                    {
                        balance -= transaction.Amount;
                    }
                    if (transaction.To == address)
                    {
                        balance += transaction.Amount;
                    }
                }
            }
            return balance;
        }
        private Block CreateGenesisBlock()
        {
            List<Transactions> transactions = new List<Transactions> { new Transactions("", "", 0) };
            return new Block(DateTime.Now, transactions, "0");
        }

        private static String PrintChain(BlockChain blockChain)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("----------------- Start Blockchain -----------------\n");
            builder.Append("\n");
            foreach (Block block in blockChain.Chain)
            {
                builder.Append("\n");
                builder.Append("------ Start Block ------\n");
                builder.Append("Hash: ");
                builder.Append(block.Hash);
                builder.Append("\n");
                builder.Append("Previous Hash: ");
                builder.Append(block.PreviousHash);
                builder.Append("\n");
                builder.Append("--- Start Transactions ---\n");
                foreach (Transactions transaction in block.transactions)
                {
                    builder.Append("From: ");
                    builder.Append(transaction.From);
                    builder.Append(" To ");
                    builder.Append(transaction.To);
                    builder.Append(" Amount ");
                    builder.Append(transaction.Amount.ToString());
                    builder.Append("\n");
                }
                builder.Append("--- End Transactions ---\n");
                builder.Append("------ End Block ------\n");
            }
            builder.Append("----------------- End Blockchain -----------------\n");
            String res = builder.ToString();
            return res;
        }
    }
}