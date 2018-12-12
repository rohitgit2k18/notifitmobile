
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Notifit.Services.Models;
using Notifit.Services.Models.RequestModels;
using Notifit.Services.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Notifit.Services.ApiHandler
{
    public class RestApi
    {
        public WebRequest webRequest = null;
        private HttpClient client;      
        private string _col = ":";       

        public RestApi()

        {
            client = new HttpClient();
        }
        //*****************Get Generic Api's**************************
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="IsHeaderRequired"></param>
        /// <param name="objHeaderModel"></param>
        /// <param name="Tobject"></param>
        /// <returns></returns>
        public async Task<T> GetAsyncData_GetApi<T>(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, T Tobject) where T : new()
        {
            // var _storedToken=Settings;
            try
            {
              //  client.MaxResponseContentBufferSize = 256000;
                if (IsHeaderRequired)
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
                    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    //client.DefaultRequestHeaders.Add("Authorization", "application/json");
                    //client.DefaultRequestHeaders.Add("Content-Length", "84");
                    //client.DefaultRequestHeaders.Add("User-Agent", "Fiddler");
                    //client.DefaultRequestHeaders.Add("Host", "localhost:49165");
                    // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                // var request = new HttpRequestMessage(HttpMethod.Get, uri);

                // request.Content = new FormUrlEncodedContent(keyValues);

                //  HttpResponseMessage response = await client.SendAsync(request);

                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {

                    var responseContent = response.Content;
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    //  SucessResponse = SucessResponse.Insert(1, "\"Status\"" + _col + "\"Success\",");
                    Tobject = JsonConvert.DeserializeObject<T>(SucessResponse);
                    return Tobject;

                }
                else
                {

                    //long ResonseStatus = Convert.ToInt64(response.StatusCode);
                    //switch (ResonseStatus)
                    //{
                    //    case 302:
                    //        _response = "{\"Status\"" + _col + "\"Invalid User Name and password..\"}";
                    //        break;
                    //    case 400:
                    //        _response = "{\"Status\"" + _col + "\"Bad Request\"}";
                    //        break;
                    //    case 401:
                    //        _response = "{\"Status\"" + _col + "\"Invalid User Name and password..\"}";
                    //        break;
                    //    case 404:
                    //        _response = "{\"Status\"" + _col + "\"Not Found\"}";
                    //        break;

                    //    default:
                    //        _response = "{\"Status\"" + _col + "\"Internal Server errror\"}";
                    //        break;

                    var responseContent = response.Content;
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    ErrorResponse = ErrorResponse.Insert(1, "\"Status\"" + _col + "\"fail\",");
                    Tobject = JsonConvert.DeserializeObject<T>(ErrorResponse);
                    return Tobject;

                    // }
                }

                //Tobject = JsonConvert.DeserializeObject<T>(_response);
                //return Tobject;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
               
               
            }
        }       
      /// <summary>
      /// 
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="uri"></param>
      /// <param name="IsHeaderRequired"></param>
      /// <param name="objHeaderModel"></param>
      /// <param name="Tobject"></param>
      /// <returns></returns>
       public async Task<ObservableCollection<T>> GetAsyncData_GetApiList<T>(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ObservableCollection<T> Tobject) where T : new()
        {
            // var _storedToken=Settings;
            try
            {
                HttpResponseMessage response = null;
                //  client.MaxResponseContentBufferSize = 256000;
                if (IsHeaderRequired)
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);

                }


                response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {

                    var responseContent = response.Content;
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    //  SucessResponse = SucessResponse.Insert(1, "\"Status\"" + _col + "\"Success\",");
                    Tobject = JsonConvert.DeserializeObject<ObservableCollection<T>>(SucessResponse);
                    return Tobject;

                }
                else
                {





                    var responseContent = response.Content;
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    //  ErrorResponse = ErrorResponse.Insert(1, "\"Status\"" + _col + "\"fail\",");
                    Tobject = JsonConvert.DeserializeObject<ObservableCollection<T>>(ErrorResponse);
                    return Tobject;


                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }
        public async Task<ObservableCollection<T>> GetAsyncData_GetCountryApiList<T>(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ObservableCollection<T> Tobject) where T : new()
        {
            
            // var _storedToken=Settings;
            try
            {
               // HttpResponseMessage response = null;
                //  client.MaxResponseContentBufferSize = 256000;
                if (IsHeaderRequired)
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);

                }

                using (var w = new WebClient())
                {
                    var json_data = string.Empty;
                    // attempt to download JSON data as a string
                    try
                    {
                        //json_data =  w.DownloadString(uri);
                        json_data= await w.DownloadStringTaskAsync(uri);
                    }
                    catch (Exception)
                    {

                    }
                    // if string with JSON data is not empty, deserialize it to class and return its instance 
                    //if (!string.IsNullOrEmpty(json_data))
                    //{
                    //    return JsonConvert.DeserializeObject<ObservableCollection<T>>(json_data);
                    //}
                    //else
                    //{
                    //    return null;
                    //}
                    //  return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<ObservableCollection<T>>(json_data) : new T();
              
                //  response = await client.GetAsync(uri);


                if (!string.IsNullOrEmpty(json_data))
                {

                   // var responseContent = response.Content;
                   // var SucessResponse = await response.Content.ReadAsStringAsync();
                    //  SucessResponse = SucessResponse.Insert(1, "\"Status\"" + _col + "\"Success\",");
                    var res =  JsonConvert.DeserializeObject<ObservableCollection<T>>(json_data);
                    return   res;

                }
                else
                {





                   // var responseContent = response.Content;
                   // var ErrorResponse = await response.Content.ReadAsStringAsync();
                    //  ErrorResponse = ErrorResponse.Insert(1, "\"Status\"" + _col + "\"fail\",");
                   // Tobject = JsonConvert.DeserializeObject<ObservableCollection<T>>(json_data);
                    return null;


                }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }

        //public static ObservableCollection<T>_download_serialized_json_data<T>(string url) where T : new()
        //{
        //    using (var w = new WebClient())
        //    {
        //        var json_data = string.Empty;
        //        // attempt to download JSON data as a string
        //        try
        //        {
        //            json_data = w.DownloadString(url);
        //        }
        //        catch (Exception)
        //        {

        //        }
        //        // if string with JSON data is not empty, deserialize it to class and return its instance 
        //        if(!string.IsNullOrEmpty(json_data))
        //        {
        //            return JsonConvert.DeserializeObject<ObservableCollection<T>>(json_data);
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //      //  return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<ObservableCollection<T>>(json_data) : new T();
        //    }
        //}
            //*********************************POST APIS**************************

            /// <summary>
            /// Login Api
            /// </summary>
            /// <param name="uri"></param>
            /// <param name="IsHeaderRequired"></param>
            /// <param name="objHeaderModel"></param>
            /// <param name="_objLoginRequest"></param>
            /// <returns></returns>
            public async Task<LoginResponse> LoginAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, LoginRequest _objLoginRequest)
          {

            client.MaxResponseContentBufferSize = 256000;
            LoginResponse _objLoginResponse = new LoginResponse();
           
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Email",_objLoginRequest.Email),
                new KeyValuePair<string, string>("Password",_objLoginRequest.Password),
                new KeyValuePair<string, string>("grant_type",_objLoginRequest.grant_type)
            };
            if (IsHeaderRequired)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Length", "69");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Fiddler");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Host", "localhost:49165");
            }
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            request.Content = new FormUrlEncodedContent(keyValues);

            HttpResponseMessage response = await client.SendAsync(request);
            var statuscode = Convert.ToInt64(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
              ResponseStatus.StatusCode= Convert.ToInt32(response.StatusCode);
                var SucessResponse = await response.Content.ReadAsStringAsync();
                _objLoginResponse = JsonConvert.DeserializeObject<LoginResponse>(SucessResponse);               
                return _objLoginResponse;
            }
            else
            {
                ResponseStatus.StatusCode = Convert.ToInt32(response.StatusCode);
                var ErrorResponse = await response.Content.ReadAsStringAsync();
                _objLoginResponse = JsonConvert.DeserializeObject<LoginResponse>(ErrorResponse);
                return _objLoginResponse;
            }

        }

        /// <summary>
        /// Forgot Password Api
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="IsHeaderRequired"></param>
        /// <param name="objHeaderModel"></param>
        /// <param name="_objForgotPasswordRequest"></param>
        /// <returns></returns>
        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ForgotPasswordRequest _objForgotPasswordRequest)
        {

            // client.MaxResponseContentBufferSize = 256000;
            ForgotPasswordResponse _objForgotPasswordResponse = new ForgotPasswordResponse();

            var keyValues = new List<KeyValuePair<string, string>>
             {
                 new KeyValuePair<string, string>("Email",_objForgotPasswordRequest.Email)
             };
            if (IsHeaderRequired)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Length", "69");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Fiddler");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Host", "localhost:49165");
            }
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            request.Content = new FormUrlEncodedContent(keyValues);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var SucessResponse = await response.Content.ReadAsStringAsync();
                _objForgotPasswordResponse = JsonConvert.DeserializeObject<ForgotPasswordResponse>(SucessResponse);
                return _objForgotPasswordResponse;
            }
            else
            {
                var ErrorResponse = await response.Content.ReadAsStringAsync();
                //  ErrorResponse = ErrorResponse.Insert(1, "\"Status\"" + _col + "\"Fail\",");
                _objForgotPasswordResponse = JsonConvert.DeserializeObject<ForgotPasswordResponse>(ErrorResponse);
                return _objForgotPasswordResponse;
            }

        }
        /// <summary>
        /// Reset Password Api
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="IsHeaderRequired"></param>
        /// <param name="objHeaderModel"></param>
        /// <param name="_objResetPasswordRequest"></param>
        /// <returns></returns>
        public async Task<ResetPasswordResponse> ResetPasswordAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ResetPasswordRequest _objResetPasswordRequest)
        {


            ResetPasswordResponse objResetPasswordResponse;
            string s = JsonConvert.SerializeObject(_objResetPasswordRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);

                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objResetPasswordResponse = JsonConvert.DeserializeObject<ResetPasswordResponse>(SucessResponse);
                    return objResetPasswordResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objResetPasswordResponse = JsonConvert.DeserializeObject<ResetPasswordResponse>(ErrorResponse);
                    return objResetPasswordResponse;
                }
            }

        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="uri"></param>
      /// <param name="IsHeaderRequired"></param>
      /// <param name="objHeaderModel"></param>
      /// <param name="_objUserRegistrationRequest"></param>
      /// <returns></returns>
        public async Task<UserRegistrationResponse> UserRegistrationAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, UserRegistrationRequest _objUserRegistrationRequest)
        {

            UserRegistrationResponse objUserRegistrationResponse;
            string s = JsonConvert.SerializeObject(_objUserRegistrationRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);

                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objUserRegistrationResponse = JsonConvert.DeserializeObject<UserRegistrationResponse>(SucessResponse);
                    return objUserRegistrationResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objUserRegistrationResponse = JsonConvert.DeserializeObject<UserRegistrationResponse>(ErrorResponse);
                    return objUserRegistrationResponse;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="IsHeaderRequired"></param>
        /// <param name="objHeaderModel"></param>
        /// <param name="_objExerciseListingRequest"></param>
        /// <returns></returns>
        public async Task<ExerciseListingResponse> ListofExercisesPostAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ExerciseListingRequest _objExerciseListingRequest)
        {
           
            ExerciseListingResponse objExerciseListingResponse;
            string s = JsonConvert.SerializeObject(_objExerciseListingRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);

                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objExerciseListingResponse = JsonConvert.DeserializeObject<ExerciseListingResponse>(SucessResponse);
                    return objExerciseListingResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objExerciseListingResponse = JsonConvert.DeserializeObject<ExerciseListingResponse>(ErrorResponse);
                    return objExerciseListingResponse;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="IsHeaderRequired"></param>
        /// <param name="objHeaderModel"></param>
        /// <param name="objAddWorkoutRequest"></param>
        /// <returns></returns>
        public async Task<AddWorkoutResponse> AddWorkoutPostAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, AddWorkoutRequest objAddWorkoutRequest)
        {

            AddWorkoutResponse objAddWorkoutResponse;
            string s = JsonConvert.SerializeObject(objAddWorkoutRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objAddWorkoutResponse = JsonConvert.DeserializeObject<AddWorkoutResponse>(SucessResponse);
                    return objAddWorkoutResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objAddWorkoutResponse = JsonConvert.DeserializeObject<AddWorkoutResponse>(ErrorResponse);
                    return objAddWorkoutResponse;
                }
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="uri"></param>
       /// <param name="IsHeaderRequired"></param>
       /// <param name="objHeaderModel"></param>
       /// <param name="_objAddExerciseWorkoutRequest"></param>
       /// <returns></returns>
        public async Task<AddExerciseWorkoutResponse> AddExerciseWorkoutPostAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, AddExerciseWorkoutRequest _objAddExerciseWorkoutRequest)
        {

            AddExerciseWorkoutResponse objAddExerciseWorkoutResponse;
            string s = JsonConvert.SerializeObject(_objAddExerciseWorkoutRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objAddExerciseWorkoutResponse = JsonConvert.DeserializeObject<AddExerciseWorkoutResponse>(SucessResponse);
                    return objAddExerciseWorkoutResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objAddExerciseWorkoutResponse = JsonConvert.DeserializeObject<AddExerciseWorkoutResponse>(ErrorResponse);
                    return objAddExerciseWorkoutResponse;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="IsHeaderRequired"></param>
        /// <param name="objHeaderModel"></param>
        /// <param name="_objScheduleListingRequest"></param>
        /// <returns></returns>
        public async Task<ScheduleListingResponse> GetScheduleListingAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ScheduleListingRequest _objScheduleListingRequest)
        {

            ScheduleListingResponse objScheduleListingResponse;
            string s = JsonConvert.SerializeObject(_objScheduleListingRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objScheduleListingResponse = JsonConvert.DeserializeObject<ScheduleListingResponse>(SucessResponse);
                    return objScheduleListingResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objScheduleListingResponse = JsonConvert.DeserializeObject<ScheduleListingResponse>(ErrorResponse);
                    return objScheduleListingResponse;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="IsHeaderRequired"></param>
        /// <param name="objHeaderModel"></param>
        /// <param name="_objEditProfileRequest"></param>
        /// <returns></returns>
        public async Task<EditProfileResponse> EditProfilePostAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, EditProfileRequest _objEditProfileRequest)
        {

            EditProfileResponse objEditProfileResponse;
            string s = JsonConvert.SerializeObject(_objEditProfileRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objEditProfileResponse = JsonConvert.DeserializeObject<EditProfileResponse>(SucessResponse);
                    return objEditProfileResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objEditProfileResponse = JsonConvert.DeserializeObject<EditProfileResponse>(ErrorResponse);
                    return objEditProfileResponse;
                }
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="uri"></param>
       /// <param name="IsHeaderRequired"></param>
       /// <param name="objHeaderModel"></param>
       /// <param name="_objEditProfileResponse"></param>
       /// <returns></returns>
        public async Task<UpdateProfileResponse> UpdateProfileAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, EditProfileResponse _objEditProfileResponse)
        {

            UpdateProfileResponse objUpdateProfileResponse;
            string s = JsonConvert.SerializeObject(_objEditProfileResponse.Response.details);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objUpdateProfileResponse = JsonConvert.DeserializeObject<UpdateProfileResponse>(SucessResponse);
                    return objUpdateProfileResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objUpdateProfileResponse = JsonConvert.DeserializeObject<UpdateProfileResponse>(ErrorResponse);
                    return objUpdateProfileResponse;
                }
            }
        }

        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objDriver_TollsListRequest"></param>
        // /// <returns></returns>
        public async Task<WorkOutDetailResponse> WorkoutDetailsPostAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, WorkOutDetailRequest _objWorkOutDetailRequest)
        {

            WorkOutDetailResponse objWorkOutDetailResponse;
            string s = JsonConvert.SerializeObject(_objWorkOutDetailRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objWorkOutDetailResponse = JsonConvert.DeserializeObject<WorkOutDetailResponse>(SucessResponse);
                    return objWorkOutDetailResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objWorkOutDetailResponse = JsonConvert.DeserializeObject<WorkOutDetailResponse>(ErrorResponse);
                    return objWorkOutDetailResponse;
                }
            }
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="uri"></param>
      /// <param name="IsHeaderRequired"></param>
      /// <param name="objHeaderModel"></param>
      /// <param name="_objAddActualExerciseRequest"></param>
      /// <returns></returns>
        public async Task<AddActualExerciseResponse> AddActualExerciseAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, AddActualExerciseRequest _objAddActualExerciseRequest)
        {

            AddActualExerciseResponse objAddActualExerciseResponse;
            string s = JsonConvert.SerializeObject(_objAddActualExerciseRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objAddActualExerciseResponse = JsonConvert.DeserializeObject<AddActualExerciseResponse>(SucessResponse);
                    return objAddActualExerciseResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objAddActualExerciseResponse = JsonConvert.DeserializeObject<AddActualExerciseResponse>(ErrorResponse);
                    return objAddActualExerciseResponse;
                }
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="uri"></param>
       /// <param name="IsHeaderRequired"></param>
       /// <param name="objHeaderModel"></param>
       /// <param name="_objAddWorkoutScheduleRequest"></param>
       /// <returns></returns>
        public async Task<AddWorkoutScheduleResponse> AddScheduleDetailAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, AddWorkoutScheduleRequest _objAddWorkoutScheduleRequest)
        {

            AddWorkoutScheduleResponse objAddWorkoutScheduleResponse;
            string s = JsonConvert.SerializeObject(_objAddWorkoutScheduleRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objAddWorkoutScheduleResponse = JsonConvert.DeserializeObject<AddWorkoutScheduleResponse>(SucessResponse);
                    return objAddWorkoutScheduleResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objAddWorkoutScheduleResponse = JsonConvert.DeserializeObject<AddWorkoutScheduleResponse>(ErrorResponse);
                    return objAddWorkoutScheduleResponse;
                }
            }
        }

        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objLoadDetails"></param>
        // /// <returns></returns>
        public async Task<ScheduleDetailsResponse> ScheduleDetailsAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ScheduleDetailsRequest _objScheduleDetailsRequest)
        {

            ScheduleDetailsResponse objScheduleDetailsResponse;
            string s = JsonConvert.SerializeObject(_objScheduleDetailsRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objScheduleDetailsResponse = JsonConvert.DeserializeObject<ScheduleDetailsResponse>(SucessResponse);
                    return objScheduleDetailsResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objScheduleDetailsResponse = JsonConvert.DeserializeObject<ScheduleDetailsResponse>(ErrorResponse);
                    return objScheduleDetailsResponse;
                }
            }
        }

        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objDriver_LoadSignOffResquest"></param>
        // /// <returns></returns>
        public async Task<ExerciseDetailsByWorkoutIDResponse> ExerciseScheduleDetailsAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ExerciseScheduleDetailRequest _objExerciseScheduleDetailRequest)
        {

            ExerciseDetailsByWorkoutIDResponse objExerciseDetailsByWorkoutIDResponse;
            string s = JsonConvert.SerializeObject(_objExerciseScheduleDetailRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objExerciseDetailsByWorkoutIDResponse = JsonConvert.DeserializeObject<ExerciseDetailsByWorkoutIDResponse>(SucessResponse);
                    return objExerciseDetailsByWorkoutIDResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objExerciseDetailsByWorkoutIDResponse = JsonConvert.DeserializeObject<ExerciseDetailsByWorkoutIDResponse>(ErrorResponse);
                    return objExerciseDetailsByWorkoutIDResponse;
                }
            }
        }

        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objDriver_WorkSheetSignOffRequest"></param>
        // /// <returns></returns>
        // public async Task<Driver_WorkSheetSignOffResponse> WorkSheetSignOffSignAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, Driver_WorkSheetSignOffRequest _objDriver_WorkSheetSignOffRequest)
        // {

        //     Driver_WorkSheetSignOffResponse objDriver_WorkSheetSignOffResponse;
        //     string s = JsonConvert.SerializeObject(_objDriver_WorkSheetSignOffRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objDriver_WorkSheetSignOffResponse = JsonConvert.DeserializeObject<Driver_WorkSheetSignOffResponse>(SucessResponse);
        //             return objDriver_WorkSheetSignOffResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objDriver_WorkSheetSignOffResponse = JsonConvert.DeserializeObject<Driver_WorkSheetSignOffResponse>(ErrorResponse);
        //             return objDriver_WorkSheetSignOffResponse;
        //         }
        //     }
        // }

        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objDriver_MaintananceListRequest"></param>
        // /// <returns></returns>
        // public async Task<Driver_MaintananceListResponse> MaintananceLoadAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, Driver_MaintananceListRequest _objDriver_MaintananceListRequest)
        // {

        //     Driver_MaintananceListResponse objDriver_MaintananceListResponse;
        //     string s = JsonConvert.SerializeObject(_objDriver_MaintananceListRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objDriver_MaintananceListResponse = JsonConvert.DeserializeObject<Driver_MaintananceListResponse>(SucessResponse);
        //             return objDriver_MaintananceListResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objDriver_MaintananceListResponse = JsonConvert.DeserializeObject<Driver_MaintananceListResponse>(ErrorResponse);
        //             return objDriver_MaintananceListResponse;
        //         }
        //     }
        // }

        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objDriver_AddMaintananceRequest"></param>
        // /// <returns></returns>
        // public async Task<Driver_AddMaintananceResponse> AddMaintananceAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, Driver_AddMaintananceRequest _objDriver_AddMaintananceRequest)
        // {

        //     Driver_AddMaintananceResponse objDriver_AddMaintananceResponse;
        //     string s = JsonConvert.SerializeObject(_objDriver_AddMaintananceRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objDriver_AddMaintananceResponse = JsonConvert.DeserializeObject<Driver_AddMaintananceResponse>(SucessResponse);
        //             return objDriver_AddMaintananceResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objDriver_AddMaintananceResponse = JsonConvert.DeserializeObject<Driver_AddMaintananceResponse>(ErrorResponse);
        //             return objDriver_AddMaintananceResponse;
        //         }
        //     }
        // }

        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objDriver_HomeRequest"></param>
        // /// <returns></returns>
        // public async Task<Driver_HomeResponse> Driver_HomeAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, Driver_HomeRequest _objDriver_HomeRequest)
        // {

        //     Driver_HomeResponse objDriver_HomeResponse;
        //     string s = JsonConvert.SerializeObject(_objDriver_HomeRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objDriver_HomeResponse = JsonConvert.DeserializeObject<Driver_HomeResponse>(SucessResponse);
        //             return objDriver_HomeResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objDriver_HomeResponse = JsonConvert.DeserializeObject<Driver_HomeResponse>(ErrorResponse);
        //             return objDriver_HomeResponse;
        //         }
        //     }
        // }

        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objDriver_TimeSheetRequest"></param>
        // /// <returns></returns>
        // public async Task<Driver_TimeSheetResponse> Driver_TimeSheetAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, Driver_TimeSheetRequest _objDriver_TimeSheetRequest)
        // {

        //     Driver_TimeSheetResponse objDriver_TimeSheetResponse;
        //     string s = JsonConvert.SerializeObject(_objDriver_TimeSheetRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objDriver_TimeSheetResponse = JsonConvert.DeserializeObject<Driver_TimeSheetResponse>(SucessResponse);
        //             return objDriver_TimeSheetResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objDriver_TimeSheetResponse = JsonConvert.DeserializeObject<Driver_TimeSheetResponse>(ErrorResponse);
        //             return objDriver_TimeSheetResponse;
        //         }
        //     }
        // }

        // //----------------------------------------------Non Driver Section Api-------------------------------------------------

        //     /// <summary>
        // /// Non Driver Home Data;
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objNonDriver_HomeDataRequest"></param>
        // /// <returns></returns>
        // public async Task<NonDriver_HomeDataResponse> NonDriver_HomeDataAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, NonDriver_HomeDataRequest _objNonDriver_HomeDataRequest)
        // {

        //     NonDriver_HomeDataResponse objNonDriver_HomeDataResponse;
        //     string s = JsonConvert.SerializeObject(_objNonDriver_HomeDataRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objNonDriver_HomeDataResponse = JsonConvert.DeserializeObject<NonDriver_HomeDataResponse>(SucessResponse);
        //             return objNonDriver_HomeDataResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objNonDriver_HomeDataResponse = JsonConvert.DeserializeObject<NonDriver_HomeDataResponse>(ErrorResponse);
        //             return objNonDriver_HomeDataResponse;
        //         }
        //     }
        // }

        // /// <summary>
        // /// Daily Checklist Data
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objNonDriver_DailyCheckListRequest"></param>
        // /// <returns></returns>
        // public async Task<NonDriver_DailyCheckListResponse> ND_DailyCheckLIstGetDataAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, NonDriver_DailyCheckListRequest _objNonDriver_DailyCheckListRequest)
        // {

        //     NonDriver_DailyCheckListResponse objNonDriver_DailyCheckListResponse;
        //     string s = JsonConvert.SerializeObject(_objNonDriver_DailyCheckListRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objNonDriver_DailyCheckListResponse = JsonConvert.DeserializeObject<NonDriver_DailyCheckListResponse>(SucessResponse);
        //             return objNonDriver_DailyCheckListResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objNonDriver_DailyCheckListResponse = JsonConvert.DeserializeObject<NonDriver_DailyCheckListResponse>(ErrorResponse);
        //             return objNonDriver_DailyCheckListResponse;
        //         }
        //     }
        // }
        // /// <summary>
        // /// Send Daily CheckList
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objNonDriver_DailyCheckListRequest"></param>
        // /// <returns></returns>
        // public async Task<ND_DailyCheckLIstPostResponse> ND_DailyCheckLIstPostDataAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ND_DailyCheckLIstPostRequest _objND_DailyCheckLIstPostRequest)
        // {

        //     ND_DailyCheckLIstPostResponse objND_DailyCheckLIstPostResponse;
        //     string s = JsonConvert.SerializeObject(_objND_DailyCheckLIstPostRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objND_DailyCheckLIstPostResponse = JsonConvert.DeserializeObject<ND_DailyCheckLIstPostResponse>(SucessResponse);
        //             return objND_DailyCheckLIstPostResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objND_DailyCheckLIstPostResponse = JsonConvert.DeserializeObject<ND_DailyCheckLIstPostResponse>(ErrorResponse);
        //             return objND_DailyCheckLIstPostResponse;
        //         }
        //     }
        // }

        // /// <summary>
        // /// ND Signature
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objND_DailyCheckLIstPostRequest"></param>
        // /// <returns></returns>
        // public async Task<ND_SignatureResponse> ND_SignatureDataAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ND_SignatureRequest _objND_SignatureRequest)
        // {

        //     ND_SignatureResponse objND_SignatureResponse;
        //     string s = JsonConvert.SerializeObject(_objND_SignatureRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objND_SignatureResponse = JsonConvert.DeserializeObject<ND_SignatureResponse>(SucessResponse);
        //             return objND_SignatureResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objND_SignatureResponse = JsonConvert.DeserializeObject<ND_SignatureResponse>(ErrorResponse);
        //             return objND_SignatureResponse;
        //         }
        //     }
        // }

        // /// <summary>
        // /// ND Timesheet data
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objND_SignatureRequest"></param>
        // /// <returns></returns>
        // public async Task<ND_TimeSheetResponse> ND_TimeSheetDataAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ND_TimeSheetRequest _objND_TimeSheetRequest)
        // {

        //     ND_TimeSheetResponse objND_TimeSheetResponse;
        //     string s = JsonConvert.SerializeObject(_objND_TimeSheetRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objND_TimeSheetResponse = JsonConvert.DeserializeObject<ND_TimeSheetResponse>(SucessResponse);
        //             return objND_TimeSheetResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objND_TimeSheetResponse = JsonConvert.DeserializeObject<ND_TimeSheetResponse>(ErrorResponse);
        //             return objND_TimeSheetResponse;
        //         }
        //     }
        // }


        // //--------------------------------------------Mechanic Section Api-------------------------------------------------------------------------------------------------------------------

        //     /// <summary>
        // /// Mechanic Home data;
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objM_HomeDataRequest"></param>
        // /// <returns></returns>
        // public async Task<M_HomeDataResponse> M_HomeDataAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, M_HomeDataRequest _objM_HomeDataRequest)
        // {

        //     M_HomeDataResponse objM_HomeDataResponse;
        //     string s = JsonConvert.SerializeObject(_objM_HomeDataRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objM_HomeDataResponse = JsonConvert.DeserializeObject<M_HomeDataResponse>(SucessResponse);
        //             return objM_HomeDataResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objM_HomeDataResponse = JsonConvert.DeserializeObject<M_HomeDataResponse>(ErrorResponse);
        //             return objM_HomeDataResponse;
        //         }
        //     }
        // }

        // /// <summary>
        // /// View Request Details Mechanic.
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objM_GetViewDetailsRequest"></param>
        // /// <returns></returns>
        // public async Task<M_GetViewDetailsResponse> M_ViewDetailsDataAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, M_GetViewDetailsRequest _objM_GetViewDetailsRequest)
        // {

        //     M_GetViewDetailsResponse objM_GetViewDetailsResponse;
        //     string s = JsonConvert.SerializeObject(_objM_GetViewDetailsRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objM_GetViewDetailsResponse = JsonConvert.DeserializeObject<M_GetViewDetailsResponse>(SucessResponse);
        //             return objM_GetViewDetailsResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objM_GetViewDetailsResponse = JsonConvert.DeserializeObject<M_GetViewDetailsResponse>(ErrorResponse);
        //             return objM_GetViewDetailsResponse;
        //         }
        //     }
        // }

        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objM_CheckListRequest"></param>
        // /// <returns></returns>
        // public async Task<M_CheckListResponse> M_ChecklIstDataAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, M_CheckListRequest _objM_CheckListRequest)
        // {
        //     M_CheckListResponse objM_CheckListResponse;
        //     string s = JsonConvert.SerializeObject(_objM_CheckListRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objM_CheckListResponse = JsonConvert.DeserializeObject<M_CheckListResponse>(SucessResponse);
        //             return objM_CheckListResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objM_CheckListResponse = JsonConvert.DeserializeObject<M_CheckListResponse>(ErrorResponse);
        //             return objM_CheckListResponse;
        //         }
        //     }
        // }

        // /// <summary>
        ///// 
        ///// </summary>
        ///// <param name="uri"></param>
        ///// <param name="IsHeaderRequired"></param>
        ///// <param name="objHeaderModel"></param>
        ///// <param name="_objM_CheckListRequest"></param>
        ///// <returns></returns>
        // public async Task<M_RequestDoneResponse> M_SendRequestDoneDataAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, M_RequestDoneRequest _objM_RequestDoneRequest)
        // {
        //     M_RequestDoneResponse objM_RequestDoneResponse;
        //     string s = JsonConvert.SerializeObject(_objM_RequestDoneRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);

        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objM_RequestDoneResponse = JsonConvert.DeserializeObject<M_RequestDoneResponse>(SucessResponse);
        //             return objM_RequestDoneResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objM_RequestDoneResponse = JsonConvert.DeserializeObject<M_RequestDoneResponse>(ErrorResponse);
        //             return objM_RequestDoneResponse;
        //         }
        //     }
        // }

        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objM_SignatureRequest"></param>
        // /// <returns></returns>
        // public async Task<M_SignatureResponse> M_SendSignatureAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, M_SignatureRequest _objM_SignatureRequest)
        // {
        //     M_SignatureResponse objM_SignatureResponse;
        //     string s = JsonConvert.SerializeObject(_objM_SignatureRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);
        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objM_SignatureResponse = JsonConvert.DeserializeObject<M_SignatureResponse>(SucessResponse);
        //             return objM_SignatureResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objM_SignatureResponse = JsonConvert.DeserializeObject<M_SignatureResponse>(ErrorResponse);
        //             return objM_SignatureResponse;
        //         }
        //     }
        // }
        // //----------------------------------------------------Sub Contractor=-----------------------------------------------------------------------

        //     /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="uri"></param>
        // /// <param name="IsHeaderRequired"></param>
        // /// <param name="objHeaderModel"></param>
        // /// <param name="_objSC_HomePageRequest"></param>
        // /// <returns></returns>
        // public async Task<SC_HomePageResponse> LoadDSCWorkSheetListAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, SC_HomePageRequest _objSC_HomePageRequest)
        // {

        //     SC_HomePageResponse objSC_HomePageResponse;
        //     string s = JsonConvert.SerializeObject(_objSC_HomePageRequest);
        //     HttpResponseMessage response = null;
        //     using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //     {

        //         if (IsHeaderRequired)
        //         {
        //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.TokenCode);

        //         }
        //         response = await client.PostAsync(uri, stringContent);


        //         if (response.IsSuccessStatusCode)
        //         {
        //             var SucessResponse = await response.Content.ReadAsStringAsync();
        //             objSC_HomePageResponse = JsonConvert.DeserializeObject<SC_HomePageResponse>(SucessResponse);
        //             return objSC_HomePageResponse;
        //         }
        //         else
        //         {
        //             var ErrorResponse = await response.Content.ReadAsStringAsync();
        //             objSC_HomePageResponse = JsonConvert.DeserializeObject<SC_HomePageResponse>(ErrorResponse);
        //             return objSC_HomePageResponse;
        //         }
        //     }
        // }


        //public async Task<UploadProfileResponse> UpdateUserProfileAsync1(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, UploadProfileBase64Req _objUploadProfileBase64Req)
        //{
        //    UploadProfileResponse objUploadProfileResponse;
        //    string s = JsonConvert.SerializeObject(_objUploadProfileBase64Req);
        //    HttpResponseMessage response = null;
        //    using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //    {
        //        //  MultipartFormDataContent form = new MultipartFormDataContent();
        //        //form.Add(new StreamContent(_mediafile.GetStream()),
        //        //   "\"file\"",
        //        //   $"\"{_mediafile.Path}\"");
        //        //  form.Add(new (content, 0, content.Count()), "profile_pic", Name);

        //        if (IsHeaderRequired)
        //    {

        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", objHeaderModel.OTPToken);


        //    }
        //    response = await client.PostAsync("http://10.0.1.246:5000/api/User/UploadUserProfile", stringContent);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var SucessResponse = await response.Content.ReadAsStringAsync();
        //        objUploadProfileResponse = JsonConvert.DeserializeObject<UploadProfileResponse>(SucessResponse);
        //        return objUploadProfileResponse;
        //    }
        //    else
        //    {
        //        var ErrorResponse = await response.Content.ReadAsStringAsync();
        //        objUploadProfileResponse = JsonConvert.DeserializeObject<UploadProfileResponse>(ErrorResponse);
        //        return objUploadProfileResponse;
        //    }
        //    }
        //}
        //public async Task<UploadProfileResponse> UpdateUserProfileAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, MediaFile _mediafile)
        //{
        //    UploadProfileResponse objUploadProfileResponse;
        //  //  string s = JsonConvert.SerializeObject(_objUploadProfileBase64Req);
        //    HttpResponseMessage response = null;
        //    //using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //    //{
        //        MultipartFormDataContent form = new MultipartFormDataContent();
        //        form.Add(new StreamContent(_mediafile.GetStream()),
        //           "\"file\"",
        //           $"\"{_mediafile.Path}\"");
        //        //  form.Add(new (content, 0, content.Count()), "profile_pic", Name);

        //        if (IsHeaderRequired)
        //        {

        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", objHeaderModel.OTPToken);
        //        }
        //        response = await client.PostAsync(uri, form);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var SucessResponse = await response.Content.ReadAsStringAsync();
        //            objUploadProfileResponse = JsonConvert.DeserializeObject<UploadProfileResponse>(SucessResponse);
        //            return objUploadProfileResponse;
        //        }
        //        else
        //        {
        //            var ErrorResponse = await response.Content.ReadAsStringAsync();
        //            objUploadProfileResponse = JsonConvert.DeserializeObject<UploadProfileResponse>(ErrorResponse);
        //            return objUploadProfileResponse;
        //        }
        //   // }
        //}


        //public async Task<GetProfilePicResponseModel> GetUserprofileAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, GetProfilePicResponseModel _objGetProfilePicResponseModel)
        //{

        //    GetProfilePicResponseModel objGetProfilePicResponseModel;
        //    string s = JsonConvert.SerializeObject(_objGetProfilePicResponseModel);
        //    HttpResponseMessage response = null;
        //    using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //    {

        //        if (IsHeaderRequired)
        //        {
        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
        //        }
        //        response = await client.PostAsync(uri, stringContent);


        //        if (response.IsSuccessStatusCode)
        //        {
        //            var SucessResponse = await response.Content.ReadAsStringAsync();
        //            objGetProfilePicResponseModel = JsonConvert.DeserializeObject<GetProfilePicResponseModel>(SucessResponse);
        //            return objGetProfilePicResponseModel;
        //        }
        //        else
        //        {
        //            var ErrorResponse = await response.Content.ReadAsStringAsync();
        //            objGetProfilePicResponseModel = JsonConvert.DeserializeObject<GetProfilePicResponseModel>(ErrorResponse);
        //            return objGetProfilePicResponseModel;
        //        }
        //    }
        //}

        //******************Generics**************************************

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> PostAsyncData(string uri)
        {
            try
            {

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                return await response.Content.ReadAsStringAsync(); ;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public void FetchData(string url)
        {

            // Create an HTTP web request using the URL:
            webRequest = WebRequest.Create(new Uri(url));
            webRequest.ContentType = "application/json";
            webRequest.Method = "GET";
            webRequest.BeginGetRequestStream(new AsyncCallback(RequestStreamCallback), webRequest);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asynchronousResult"></param>
        private void RequestStreamCallback(IAsyncResult asynchronousResult)
        {
            webRequest = (HttpWebRequest)asynchronousResult.AsyncState;

            using (var postStream = webRequest.EndGetRequestStream(asynchronousResult))
            {
                //send yoour data here
            }
            webRequest.BeginGetResponse(new AsyncCallback(ResponseCallback), webRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asynchronousResult"></param>
        void ResponseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest myrequest = (HttpWebRequest)asynchronousResult.AsyncState;
            using (HttpWebResponse response = (HttpWebResponse)myrequest.EndGetResponse(asynchronousResult))
            {
                using (System.IO.Stream responseStream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(responseStream))
                    {
                        var data = reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
