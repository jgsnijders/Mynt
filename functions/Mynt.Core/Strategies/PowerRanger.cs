﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mynt.Core.Enums;
using Mynt.Core.Indicators;
using Mynt.Core.Interfaces;
using Mynt.Core.Models;

namespace Mynt.Core.Strategies
{
    public class PowerRanger : ITradingStrategy
    {
        public string Name => "Power Ranger";
        
        public List<ITradeAdvice> Prepare(List<Candle> candles)
        {
            var result = new List<ITradeAdvice>();
            var stoch = candles.Stoch(10);

            for (int i = 0; i < candles.Count; i++)
            {
                if (i < 1)
                    result.Add(new SimpleTradeAdvice(TradeAdvice.Hold));
                else
                {
                    if ((stoch.K[i] > 20 && stoch.K[i - 1] < 20) || (stoch.D[i] > 20 && stoch.D[i - 1] < 20))
                        result.Add(new SimpleTradeAdvice(TradeAdvice.Buy));
                    else if ((stoch.K[i] < 80 && stoch.K[i - 1] > 80) || (stoch.D[i] < 80 && stoch.D[i - 1] > 80))
                        result.Add(new SimpleTradeAdvice(TradeAdvice.Sell));
                    else
                        result.Add(new SimpleTradeAdvice(TradeAdvice.Hold));
                }
            }

            return result;
        }
    }
}
