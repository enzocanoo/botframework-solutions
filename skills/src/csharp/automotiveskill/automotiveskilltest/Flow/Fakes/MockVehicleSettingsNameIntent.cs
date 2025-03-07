﻿using System;
using System.Collections.Generic;
using Luis;
using Microsoft.Bot.Builder;

namespace AutomotiveSkillTest.Flow.Fakes
{
    public class MockVehicleSettingsNameIntent : settings_nameLuis
    {
        private string userInput;
        private Intent intent;
        private double score;

        public MockVehicleSettingsNameIntent(string userInput)
        {
            if (string.IsNullOrEmpty(userInput))
            {
                throw new ArgumentNullException(nameof(userInput));
            }

            this.Entities = new settings_nameLuis._Entities();
            this.Intents = new Dictionary<Intent, IntentScore>();

            this.userInput = userInput;

            var intentScore = new Microsoft.Bot.Builder.IntentScore();
            intentScore.Score = 0.9909704;
            intentScore.Properties = new Dictionary<string, object>();

            this.Intents.Add(settings_nameLuis.Intent.SETTING_NAME_SELECTION, intentScore);

            switch (userInput.ToLower())
            {
                case "first one":
                    this.Entities.INDEX = new string[] { "first" };
                    break;
                case "alerts for people in the back":
                    this.Entities.SETTING = new string[] { "alerts", "people", "back" };
                    break;
                case "equalizer (bass)":
                case "front":
                case "front combined air delivery mode control":
                    this.Entities.SETTING = new string[] { userInput.ToLower() };
                    break;
            }

            (intent, score) = this.TopIntent();
        }
    }
}