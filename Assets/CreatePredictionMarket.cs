using UnityEngine;
using Web3Unity.Scripts.Library.Ethers.Contracts;
using Web3Unity.Scripts.Library.Ethers.Providers;
using System.Threading.Tasks;
using System.Collections.Generic;

public class CreatePredictionMarket : MonoBehaviour
{
    public string contractAddress = "0x741C3F3146304Aaf5200317cbEc0265aB728FE07";
    public string contractAbi = "[{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"_startingPrice\",\"type\":\"uint256\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[{\"internalType\":\"uint8\",\"name\":\"_bet\",\"type\":\"uint8\"}],\"name\":\"placeBet\",\"outputs\":[],\"stateMutability\":\"payable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"endRound\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"_winningBet\",\"type\":\"uint256\"}],\"name\":\"settleRound\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"winningBet\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";
    public int startingPrice = 100;
    public string privateKey;
    public string playerAddress;

    async void Start()
    {
        var provider = new InfuraProvider("YOUR_PROJECT_ID", "mainnet");
        var contract = new Contract(contractAbi, contractAddress, provider);
        var calldata = contract.Calldata("constructor", new object[]
        {
            startingPrice
        });
        var transactionHash = await Web3Wallet.SendTransaction("1", privateKey, contractAddress, calldata);
        Debug.Log("Transaction hash: " + transactionHash);
    }

    public async void CreateNewPool()
    {
        var provider = new InfuraProvider("YOUR_PROJECT_ID", "mainnet");
        var contract = new Contract(contractAbi, contractAddress, provider);
        var calldata = contract.Calldata("constructor", new object[]
        {
            startingPrice
        });
        var transactionHash = await Web3Wallet.SendTransaction("1", privateKey, contractAddress, calldata);
        Debug.Log("Transaction hash: " + transactionHash);
    }

    public async void PlaceBet(int bet)
    {
        var provider = new InfuraProvider("YOUR_PROJECT_ID", "mainnet");
        var contract = new Contract(contractAbi, contractAddress, provider);
        var calldata = contract.Calldata("placeBet", new object[]
        {
            bet
        });
        var transactionHash = await Web3Wallet.SendTransaction("1", privateKey, contractAddress, calldata);
        Debug.Log("Transaction hash: " + transactionHash);
    }

    public async void EndRound()
    {
        var provider = new InfuraProvider("YOUR_PROJECT_ID", "mainnet");
        var contract = new Contract(contractAbi, contractAddress, provider);
        var calldata = contract.Calldata("endRound");
        var transactionHash = await Web3Wallet.SendTransaction("1", privateKey, contractAddress, calldata);
        Debug.Log("Transaction hash: " + transactionHash);
    }

    public async void SettleRound(int winningBet)
    {
        var provider = new InfuraProvider("YOUR_PROJECT_ID", "mainnet");
        var contract = new Contract(contractAbi, contractAddress, provider);
        var calldata = contract.Calldata("settleRound", new object[]
        {
        winningBet
        });
        var transactionHash = await Web3Wallet.SendTransaction("1", privateKey, contractAddress, calldata);
        Debug.Log("Transaction hash: " + transactionHash);
        }
        public async Task<uint> GetWinningBet()
        {
            var provider = new InfuraProvider("YOUR_PROJECT_ID", "mainnet");
            var contract = new Contract(contractAbi, contractAddress, provider);
            var result = await contract.Call("winningBet");
            return (uint)result[0];
        }

        public async Task<Dictionary<string, int>> GetBetAmounts()
        {
            var provider = new InfuraProvider("YOUR_PROJECT_ID", "mainnet");
            var contract = new Contract(contractAbi, contractAddress, provider);
            var upBetResult = await contract.Call("betAmounts", new object[]
            {
                0
            });
            var downBetResult = await contract.Call("betAmounts", new object[]
            {
                1
            });
            var betAmounts = new Dictionary<string, int>()
            {
                {"up", (int)upBetResult[0]},
                {"down", (int)downBetResult[0]}
            };
            return betAmounts;
        }
}
