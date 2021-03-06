﻿using System.Collections.Generic;
using System.Linq;
using Mynt.Core.Enums;
using Mynt.Core.Indicators;
using Mynt.Core.Interfaces;
using Mynt.Core.Models;

namespace Mynt.Core.Strategies
{
    /// <summary>
    /// https://www.tradingview.com/script/uCV8I4xA-Bollinger-RSI-Double-Strategy-by-ChartArt-v1-1/
    /// </summary>
    public class ThreeMAgos : ITradingStrategy
    {
        public string Name => "Three MAgos";

        public List<ITradeAdvice> Prepare(List<Candle> candles)
        {
            var result = new List<ITradeAdvice>();

            var sma = candles.Sma(15);
            var ema = candles.Ema(15);
            var wma = candles.Wma(15);
            var closes = candles.Select(x => x.Close).ToList();

            var bars = new List<string>();

            for (int i = 0; i < candles.Count; i++)
            {
                if ((closes[i] > sma[i]) && (closes[i] > ema[i]) && (closes[i] > wma[i]))
                    bars.Add("green");
                else if ((closes[i] > sma[i]) || (closes[i] > ema[i]) || (closes[i] > wma[i]))
                    bars.Add("blue");
                else
                    bars.Add("red");

            }
            
            for (int i = 0; i < candles.Count; i++)
            {
                if (i < 1)
                    result.Add(new SimpleTradeAdvice(TradeAdvice.Hold));
                else if (bars[i] == "blue" && bars[i - 1] == "red")
                    result.Add(new SimpleTradeAdvice(TradeAdvice.Buy));
                else if (bars[i] == "blue" && bars[i - 1] == "green")
                    result.Add(new SimpleTradeAdvice(TradeAdvice.Sell));
                else
                    result.Add(new SimpleTradeAdvice(TradeAdvice.Hold));
            }

            return result;
        }
    }
}

