﻿using System.Collections.Generic;
using Mynt.Core.Enums;
using Mynt.Core.Indicators;
using Mynt.Core.Interfaces;
using Mynt.Core.Models;

namespace Mynt.Core.Strategies
{

    /// Short-term indicator.
    /// Typically close these after a 15pt rise/fall
    public class StochAdx : ITradingStrategy
    {
        public string Name => "Stoch ADX";
        
        public List<ITradeAdvice> Prepare(List<Candle> candles)
        {
            var result = new List<ITradeAdvice>();

            var stoch = candles.Stoch(13);
            var adx = candles.Adx(14);
            var bearBull = candles.BearBull();

            for (int i = 0; i < candles.Count; i++)
            {
                if (adx[i] > 50 && (stoch.K[i] > 90 || stoch.D[i] > 90) && bearBull[i] == -1)
                    result.Add(new SimpleTradeAdvice(TradeAdvice.Sell));
                else if (adx[i] < 20 && (stoch.K[i] < 10 || stoch.D[i] < 10) && bearBull[i] == 1)
                    result.Add(new SimpleTradeAdvice(TradeAdvice.Buy));
                else
                    result.Add(new SimpleTradeAdvice(TradeAdvice.Hold));
            }

            return result;
        }
    }
}
