using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATS_AlarmTrackingSystem.Models
{
    public class MealEat
    {
        public string IcivetNo { get; set; }

        public string IcivetName { get; set; }

        public string IcivetBu { get; set; }

        public string QRMeal { get; set; }

        public string MealBatch1 { get; set; }
        public string MealTime1 { get; set; }

        public string MealBatch2 { get; set; }
        public string MealTime2 { get; set; }

        public string MealBatch3 { get; set; }
        public string MealTime3 { get; set; }

        public string MealBatch4 { get; set; }
        public string MealTime4 { get; set; }



        public string QRLive { get; set; }

        public string QRMoveVehicle { get; set; }

        public string QRMove { get; set; }

        public string QRWork { get; set; }
    }
}