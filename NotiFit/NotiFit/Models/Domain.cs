using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetTransport.Models
{
  public static class Domain
    {
        public static string Url
        {
          
            get
            {
                return "http://noti.fit:130/";
               
            }
        }

        public static string LoginApiConstant
        {
            get
            {
                return "api/User/login";
            }
        }
        public static string ForgotPasswordApiConstant
        {
            get
            {
                return "api/User/ForgetPassword";
            }
        }
        public static string ResetPasswordApiConstant
        {
            get
            {
                return "api/User/ResetPassword";
            }
        }
        public static string GetCountryApiConstant
        {
            get
            {
                return "https://restcountries.eu/rest/v2/all";
            }
        }
        public static string UserSignUpApiConstant
        {
            get
            {
                return "api/User/SignUp";
            }
        }

        public static string CompltedCountApiConstant
        {
            get
            {
                return "api/Dashborad/CompletedWorkout";
            }
        }
        
        public static string MissedCountApiConstant
        {
            get
            {
                return "api/Dashborad/DashboardWorkoutData";
            }
        }

        public static string ListofWorkoutApiConstant
        {
            get
            {
                return "api/Workout/ListOfWorkoutsForMobile";
            }
        }
        public static string AddWorkoutApiConstant
        {
            get
            {
                return "api/Workout/AddWorkout";
            }
        }
        public static string AddExerciseWorkoutApiConstant
        {
            get
            {
                return "api/Exercise/AddExercise";
            }
        }
        public static string GetScheduleListApiConstant
        {
            get
            {
                return "api/ExerciseSchedule/ListOfSchedule";
            }
        }
        public static string EditUserApiConstant
        {
            get
            {
                return "api/User/EditProfile";
            }
        }
        public static string UpdateUserApiConstant
        {
            get
            {
                return "api/User/UpdateUser";
            }
        }
        public static string WorkoutDetailspiConstant
        {
            get
            {
                return "api/Workout/WorkoutDetails";
            }
        }
        public static string ExerciseDetailsByIDApiConstant
        {
            get
            {
                return "api/Exercise/ExerciseDetailsByWorkOutId";
            }
        }
        public static string AddActualExerciseApiConstant
        {
            get
            {
                return "api/Exercise/AddActualExercise";
            }
        }
        public static string AddWorkoutScheduleApiConstant
        {
            get
            {
                return "api/Schedule/AddWorkoutSchedule";
            }
        }
        public static string AddScheduleExerciseApiConstant
        {
            get
            {
                return "api/ExerciseSchedule/AddScheduleExercise1";
            }
        }
        public static string ScheduleDetailsApiConstant
        {
            get
            {
                return "api/Schedule/ScheduleWorkoutDetails";
            }
        }
        public static string ExerciseScheduleDetailsApiConstant
        {
            get
            {
                return "api/ExerciseSchedule/ScheduleDetailsById";
            }
        }

        //-----------------------------Non Driver-------------------------------
        public static string NonDriver_HomeApiConstant
        {
            get
            {
                return "api/MobileUser/GetEmployeeById";
            }
        }

        public static string NDDailyCheckListGet_ApiConstant
        {
            get
            {
                return "api/MobileUser/GetCheckListByEmployeeId";
            }
        }
       
        public static string NDDailyCheckListPost_ApiConstant
        {
            get
            {
                return "api/MobileUser/AddChecklist";
            }
        }
        public static string ND_SignatureApiConstant
        {
            get
            {
                return "api/MobileUser/AddSignature";
            }
        }
        public static string ND_TimeSheetApiConstant
        {
            get
            {
                return "api/WorksTimes/GetWorksTimeList";
            }
        }

        //----------------------------Mechanic Section-------------------------------
        /// <summary>
        /// 
        /// </summary>
        public static string M_HomeDataApiConstant
        {
            get
            {
                return "api/MobileUser/GetEmployeeById";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string M_ViewDetailsApiConstant
        {
            get
            {
                return "api/MobileUser/GetMaintenanceRequetById";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string M_CheckboxListApiConstant
        {
            get
            {
                return "api/MobileUser/GetCheckListByEmployeeId";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string M_SendRequestDoneApiConstant
        {
            get
            {
                return "api/MobileUser/MaintenanceRequetDone";
            }
        }
        public static string M_SignatureApiConstant
        {
            get
            {
                return "api/MobileUser/AddMechanicSignature";
            }
        }
        public static string M_RequestHistoryApiConstant
        {
            get
            {
                return "api/MobileUser/GetEmployeeById";
            }
        }
        // --------------------------------------------Sub Contractor---------------------------------
        public static string SC_HomeApiConstant
        {
            get
            {
                return "api/MobileUser/GetEmployeeById";
            }
        }
    }
}
