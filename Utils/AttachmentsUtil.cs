﻿/*
Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment.
THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object code form of the Sample Code, provided that. 
You agree: 
	(i) to not use Our name, logo, or trademarks to market Your software product in which the Sample Code is embedded;
    (ii) to include a valid copyright notice on Your software product in which the Sample Code is embedded; and
	(iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys’ fees, that arise or result from the use or distribution of the Sample Code
**/

// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Configuration;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;

using Microsoft.Bot.Builder.Luis.Models;


using System.Collections.Generic;
using SourceBot.Utils;

using SourceBot.DataTypes;
using SourceBot.Dialogs;
using System.Threading;


using Microsoft.Bot.Connector;

namespace SourceBot.Utils
{
    public class AttachmentsUtil
    {
        public static Attachment GetEndCard(string name)
        {
            var endCard = new HeroCard
            {
                Title = $"{name}  - Thank you!",
                Text = Utilities.GetSentence("19.60"),
                Images = new List<CardImage> { new CardImage("https://www.tapi.com/globalassets/about-us-new.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.PostBack, "Sure", value: "survey"),
                    new CardAction(ActionTypes.PostBack, "No", value: "bye") }
            };

            return endCard.ToAttachment();
        }


        public static Attachment GetOpenCard(string name, string company)
        {
            var openCard = new HeroCard
            {
                Title = string.Format(Utilities.GetSentence("19.70"), name, company), // $"API Source Bot tailored for {name} @ {company}",
                Subtitle = Utilities.GetSentence("19.71"),
                Text = Utilities.GetSentence("19.72"),
                Images = new List<CardImage> { new CardImage("https://www.tapi.com/globalassets/about-us-new.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.PostBack, "Find me Aripiprazole", value: "find me Aripiprazole"), new CardAction(ActionTypes.PostBack, "Find me Aztreonam", value: "find me Aztreonam") }
            };

            return openCard.ToAttachment();
        }

        public static Attachment GetErrorCard(string code)
        {
            var openCard = new HeroCard
            {
                Title = Utilities.GetSentence("950"),
                Subtitle = string.Format(Utilities.GetSentence("951"), code),
                Text = Utilities.GetSentence("952"),
                Images = new List<CardImage> { new CardImage("https://cdn.dribbble.com/users/7770/screenshots/3935947/oh_snap_404_1x.jpg") }                
            };

            return openCard.ToAttachment();
        }


        public static Attachment GetNoResults(string query)
        {
            var productCard = new ThumbnailCard
            {
                Title = Utilities.GetSentence("5"),
                Subtitle = string.Format(Utilities.GetSentence("5.1"), query),
                Text = Utilities.GetSentence("5.2"),
                Images = new List<CardImage> { new CardImage("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQHEl-j7JobwiGjkbpCBVemqrUKp9EQFtPQOyOLXIBsAvycS8Kx") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.PostBack, Utilities.GetSentence("6"), value: Lead.CONTACT_TAPI),
                new CardAction(ActionTypes.PostBack, Utilities.GetSentence("7"), value: Lead.PDF),
                new CardAction(ActionTypes.PostBack, Utilities.GetSentence("8"), value: Lead.UPDATE_ONCE_EXIST)
                }
            };

            return productCard.ToAttachment();

        }


        public static Attachment GetResultCard(IList<ProductDocument> tproducts)
        {
            string suffix = "";
            int count = 0;
            if (tproducts.Count == 1) return tproducts[0].GetProductCard(ProductDocument.CONFIRM);
            List<CardAction> buttons = new List<CardAction>();
            if (tproducts.Count > 0)
            {
                suffix = (tproducts.Count == 1) ? "" : "s";

                foreach (ProductDocument prd in tproducts)
                {
                    if (count == ProductDocument.MAX_PROD_IN_RESULT) break;
                    buttons.Add(new CardAction(ActionTypes.PostBack, $"{prd.TapiProductName}", value: $"find me {prd.MoleculeID}"));
                    count++;
                }
            }
            var resultCard = new HeroCard
            {
                Title = $"I found: {tproducts.Count} product{suffix}.",
                Subtitle = Utilities.GetSentence("16"),
                Text = Utilities.GetSentence("16.1"),
                Images = new List<CardImage> { new CardImage("https://www.tapi.com/globalassets/hp-banner_0000_inspections.jpg") },
                Buttons = buttons
            };

            return resultCard.ToAttachment();
        }

    }

    
}