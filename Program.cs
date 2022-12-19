using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trades.Model;

namespace Trades
{
    public class Program
    {
        private static ListofTrades items;
        private static ListofTrades Save_list;
        private static string pair;
        static void Main(string[] args)
        {
            Console.WriteLine(">>> Welcome <<<");
            Console.WriteLine(">>> Choose pair: BTC_USD ETH_USD LTC_USD USDT_USD");

            var pairs = Console.ReadLine();
            pair = pairs;

            var delay = TimeSpan.FromSeconds(1);

            var client = new WebClient();
            var jsonString = client.DownloadString("https://api.exmo.com/v1/trades/?pair=" + pairs);
            items = JsonConvert.DeserializeObject<ListofTrades>(jsonString);
            Save_list = items;

            switch (pairs)
            {
                case "BTC_USD":
                    {
                        foreach (TradesModel itemBtcUsd in items.BTC_USD)
                        {   
                            var myThread1 = new Thread(() => GetListOfPairs(itemBtcUsd));
                            myThread1.Start();
                            Thread.Sleep(delay);
                        }
                        break;
                    }
                case "ETH_USD":
                    {
                        foreach (TradesModel itemEthUsd in items.ETH_USD)
                        {
                            var myThread2 = new Thread(() => GetListOfPairs(itemEthUsd));
                            myThread2.Start();
                            Thread.Sleep(delay);
                        }
                        break;
                    }
                case "LTC_USD":
                    {
                        foreach (TradesModel itemLtcUsd in items.LTC_USD)
                        {
                            var myThread3 = new Thread(() => GetListOfPairs(itemLtcUsd));
                            myThread3.Start();
                            Thread.Sleep(delay);
                        }
                        break;
                    }
                case "USDT_USD":
                    {
                        foreach (TradesModel itemUsdtUsd in items.USDT_USD)
                        {
                            var myThread4 = new Thread(() => GetListOfPairs(itemUsdtUsd));
                            myThread4.Start();
                            Thread.Sleep(delay);
                            
                        }
                        break;
                    }
            }
            var timerState = new TimerState { Counter = 0 };
            var timerclear = new Timer(callback: new TimerCallback(TimerClear), state: timerState, dueTime: 60000, period: 60000);
            var timerpair = new Timer(callback: new TimerCallback(TimerPair), state: timerState, dueTime: 500, period: 500);
            while (timerState.Counter <= 9999)
            {
                Thread.Sleep(1000);
            }
            timerclear.Dispose();
            timerpair.Dispose();
        }
        private static void TimerClear(object timerState)
        {
            switch (pair)
            {
                case "BTC_USD": { Console.Clear(); Console.WriteLine(">>> Clear pairs: " + pair); break; }
                case "ETH_USD": { Console.Clear(); Console.WriteLine(">>> Clear pairs: " + pair); break; }
                case "LTC_USD": { Console.Clear(); Console.WriteLine(">>> Clear pairs: " + pair); break; }
                case "USDT_USD": { Console.Clear(); Console.WriteLine(">>> Clear pairs: " + pair); break; }

            }
            var state = timerState as TimerState;
            Interlocked.Increment(ref state.Counter);
        }
        public static void TimerPair(object timerState)
        {
            var client = new WebClient();
            var jsonString = client.DownloadString("https://api.exmo.com/v1/trades/?pair=" + pair);
            items = JsonConvert.DeserializeObject<ListofTrades>(jsonString);
            switch (pair)
            {
                case "BTC_USD":
                    {
                        if (Save_list.BTC_USD is null)
                        {
                            Save_list.BTC_USD = items.BTC_USD;
                        }
                        if ((Save_list.BTC_USD.Last().trade_id != items.BTC_USD.First().trade_id) && (Save_list.BTC_USD.First().trade_id != items.BTC_USD.First().trade_id))
                        {
                            Save_list.BTC_USD.Add(items.BTC_USD.First());
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($">>> PAIR {pair}: Change pair ETH_USD LTC_USD USDT_USD");
                            var myThread1 = new Thread(() => GetListOfPairs(Save_list.BTC_USD.Last()));
                            myThread1.Start();
                        }
                    }
                    break;
                case "ETH_USD":
                    {
                        if(Save_list.ETH_USD is null)
                        {
                            Save_list.ETH_USD = items.ETH_USD;
                        }
                        if (Save_list.ETH_USD.Last().trade_id != items.ETH_USD.First().trade_id && Save_list.ETH_USD.First().trade_id != items.ETH_USD.First().trade_id)
                        {
                            Save_list.ETH_USD.Add(items.ETH_USD.First());
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($">>> PAIR {pair}: Change pair BTC_USD LTC_USD USDT_USD");
                            var myThread1 = new Thread(() => GetListOfPairs(Save_list.ETH_USD.Last()));
                            myThread1.Start();
                        }
                    }
                    break;
                case "LTC_USD":
                    {
                        if (Save_list.LTC_USD is null)
                        {
                            Save_list.LTC_USD = items.LTC_USD;
                        }
                        if (Save_list.LTC_USD.Last().trade_id != items.LTC_USD.First().trade_id && Save_list.LTC_USD.First().trade_id != items.LTC_USD.First().trade_id)
                        {
                            Save_list.LTC_USD.Add(items.LTC_USD.First());
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($">>> PAIR {pair}: Change pair BTC_USD ETH_USD USDT_USD");
                            var myThread1 = new Thread(() => GetListOfPairs(Save_list.LTC_USD.Last()));
                            myThread1.Start();
                        }
                    }
                    break;
                case "USDT_USD":
                    {
                        if (Save_list.USDT_USD is null)
                        {
                            Save_list.USDT_USD = items.USDT_USD;
                        }
                        if (Save_list.USDT_USD.Last().trade_id != items.USDT_USD.First().trade_id && Save_list.USDT_USD.First().trade_id != items.USDT_USD.First().trade_id)
                        {
                            Save_list.USDT_USD.Add(items.USDT_USD.First());
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($">>> PAIR {pair}: Change pair BTC_USD ETH_USD LTC_USD");
                            var myThread1 = new Thread(() => GetListOfPairs(Save_list.USDT_USD.Last()));
                            myThread1.Start();
                        }
                    }
                    break;
            }
        }
        static void GetListOfPairs(TradesModel PairTrades)
        {
            if (PairTrades.type.Equals("buy"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            var mydate = DateTimeFromUnixTime(PairTrades.date);
            Console.WriteLine("TRADE ID: " + PairTrades.trade_id + " | " + "DATE: " + mydate + " | " + "TYPE: " + PairTrades.type + " | " + "QUANTITY: " + PairTrades.quantity + " | " + "PRICE: " + PairTrades.price + " | " + "AMOUNT: " + PairTrades.amount);
            Console.ForegroundColor = ConsoleColor.White;
            var pairs = Console.ReadLine();
            if (!pairs.Equals(pair))
            {
                pair = pairs;
                var client = new WebClient();
                var jsonString = client.DownloadString("https://api.exmo.com/v1/trades/?pair=" + pair);
                items = JsonConvert.DeserializeObject<ListofTrades>(jsonString);
            }
            Console.ReadKey();
        }
        public static DateTime DateTimeFromUnixTime(long unixTimeStamp)
        {
            return DateTimeOffset
                 .FromUnixTimeSeconds(unixTimeStamp)
                 .UtcDateTime;
        }
    }
}
