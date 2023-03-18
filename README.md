# Unity Code for Prediction Market with Playnance Smart Contract

![Untitled-1](https://user-images.githubusercontent.com/19206978/226078406-3da6083d-0483-499f-a372-6f46d72192d0.png)


This is a set of Unity scripts that interact with a smart contract to create a prediction market for players to bet on the winner of a game. The smart contract creates a new pool for each game and sets the starting price for the winner's odds. Players can make trades by placing bets on either the "up" or "down" groups depending on their prediction of the winner. Once the game is over, the smart contract calculates the distribution of winnings based on the bets placed on the winning group and distributes the winnings accordingly.

The scripts use the Web3Unity library to interact with the smart contract. The InfuraProvider is used to connect to the Ethereum network.

## How to Use
### Setting Up the Environment
To use this code, you need to have Unity installed on your computer. You also need to have an Ethereum wallet with some Ether to pay for gas fees. You can create an Ethereum wallet using any of the available options such as Metamask or MyEtherWallet.

### Configuring the Scripts
Before running the scripts, you need to configure the following parameters in the CreatePredictionMarket.cs file:

* **contractAddress:** The address of the smart contract that you want to interact with.
* **contractAbi:** The ABI (Application Binary Interface) of the smart contract.
* **startingPrice:** The starting price for the winner's odds.
* **privateKey:** The private key of your Ethereum wallet.
* **playerAddress:** The public address of your Ethereum wallet.

### Running the Scripts
To run the scripts, simply attach the CreatePredictionMarket.cs script to a GameObject in your Unity scene. You can then call the following functions from your game logic:

* **CreateNewPool():** Creates a new pool for the game with the specified starting price.
* **PlaceBet(bet):** Places a bet on the "up" or "down" group depending on the value of bet (0 for "down" and 1 for "up").
* **EndRound():** Ends the current round of the game and calculates the distribution of winnings based on the bets placed on the winning group.
* **SettleRound(winningBet):** Sets the winner of the game and distributes the winnings accordingly. The value of winningBet should be 0 for "down" or 1 for "up".
You can also modify the scripts to add more functionality or to integrate the prediction market into your game.

## CreatePredictionMarket.cs code:

```
using UnityEngine;
using Web3Unity.Scripts.Library.Ethers.Contracts;
using Web3Unity.Scripts.Library.Ethers.Providers;
using System.Threading.Tasks;
using System.Collections.Generic;

public class CreatePredictionMarket : MonoBehaviour
{
    public string contractAddress = "0x316A98FB07bF6AaA95B2b27A79A76B499d372379";
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

```

## License
This code is released under the MIT License. Feel free to use it in your projects and to modify it as needed.

