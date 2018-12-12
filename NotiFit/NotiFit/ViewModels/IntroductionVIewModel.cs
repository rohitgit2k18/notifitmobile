using NotiFit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotiFit.ViewModels
{
   public class IntroductionVIewModel
    {
        public IEnumerable<IntroductionModel> FlipSource { get; }
        = new List<IntroductionModel>() {
                new IntroductionModel() {
                    Desc = "Schedule your cardio, weight or other kinds of Workouts.",
                    Img = "intro3_pic.png",
                    BackImg="intro3_bg.png",
                    Title="Schedule Your Workouts"
                },
                new IntroductionModel() {
                    Desc = "Get notifications to your phone when it's time to workout - and sign up friends to be notified if you miss a workout.",
                    Img = "intro1_pic.png",
                    BackImg="intro1_bg.png",
                    Title="Workout Notifications"
                },
                            new IntroductionModel() {
                   Desc = "Record your time,sets,reps and more each time you work out, and track your progress towards your goals",
                    Img = "intro2_pic.png",
                    BackImg="intro2_bg.png",
                    Title="Record Your Workouts"
                },
                            new IntroductionModel() {
                    Desc = "Get two weeks free app notifications to a friend when you sign up , then pay just 0.99USD per month.Personal notifications are free.",
                    Img = "intro4_pic.png",
                    BackImg="intro4_bg.png",
                    Title="Free Trial"
                }
                            
            };

        public IEnumerable<MeasurementEntity> measurementWightEntities { get; }
        = new List<MeasurementEntity>()
        {
            new MeasurementEntity()
            {
                Id = 1,
                Name = "Kilogram"
            },
            new MeasurementEntity()
            {
                Id = 2,
                Name = "lbs"
            }
        };
    }
}
