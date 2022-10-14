using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversityAPI.HelperClasses
{
    public class Response
    {
        public Boolean success { set; get; } = true;
        public ErrorMessage error_message { set; get; }
        public object data { set; get; } = new object();
        public Response()
        {
            error_message = new ErrorMessage();
        }

        public void SetResponse(string code, string Method = "", Exception ex = null)
        {
            switch (code)
            {
                case Globals.SUCCESS:
                    success = true;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "Success";
                    error_message.msg_type = "Success";
                    break;
                case Globals.DATABASE_READING_ERROR:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "There was an error in Database while reading the data (Data missing or an error occured)";
                    error_message.msg_type = "There was an error in Database while reading the data (Data missing or an error occured)";
                    break;
                case Globals.DATABASE_WRITING_ERROR:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "There was an error in Database while writing the data (Data wrong format or an error occured)";
                    error_message.msg_type = "There was an error in Database while writing the data (Data wrong format or an error occured)";
                    break;
                case Globals.DATABASE_UPDATE_ERROR:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "There was an error in Database while updating the data (Data wrong format or an error occured)";
                    error_message.msg_type = "There was an error in Database while updating the data (Data wrong format or an error occured)";
                    break;
                case Globals.TOKEN_MISMATCH_OR_MISSING:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "The token is expired, missing or is not correct";
                    error_message.msg_type = "The token is expired, missing or is not correct";
                    break;
                case Globals.FAILED_TO_SAVE_FILE:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "There was an error while trying to save the file";
                    error_message.msg_type = "There was an error while trying to save the file";
                    break;
                case Globals.GENERIC_CATCH_ERROR:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "There was a generic catch error";
                    error_message.msg_type = "There was a generic catch error";
                    break;
                case Globals.NO_RESULTS:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "No data was returned by the database";
                    error_message.msg_type = "No data was returned by the database";
                    break;
                case Globals.PASSWORD_MISMATCH:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "The password is incorrect";
                    error_message.msg_type = "The password is incorrect";
                    break;
                case Globals.INACTIVE_USER:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "The user is inactive";
                    error_message.msg_type = "The user is inactive";
                    break;
                case Globals.ACCOUNT_EXPIRED:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "The account has expired";
                    error_message.msg_type = "The account has expired";
                    break;
                case Globals.USER_DOESNT_EXIST:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "The user does not exist";
                    error_message.msg_type = "The user does not exist";
                    break;
                case Globals.WRONG_OLD_PASSWORD:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "The old password is wrong";
                    error_message.msg_type = "The old password is wrong";
                    break;
                case Globals.NEW_PASSWORD_MISMATCH:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "The new password does not match";
                    error_message.msg_type = "The new password does not match";
                    break;
                case Globals.INVALID_PASSWORD_POLICY:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "The password does not meet the policy requirements";
                    error_message.msg_type = "The password does not meet the policy requirements";
                    break;
                case Globals.CARRIER_NOT_FOUND:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "Carrier not found in database";
                    error_message.msg_type = "Carrier not found in database";
                    break;
                case Globals.BOXES_NOT_FOUND:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "Boxes not setting in the order";
                    error_message.msg_type = "Boxes not setting in the order";
                    break;
                case Globals.CARRIER_SERVICE_ERROR:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "Carrier API service in error";
                    error_message.msg_type = "Carrier API service in error";
                    break;
                default:
                    success = false;
                    error_message.msg_code = Convert.ToInt32(code);
                    error_message.msg_text = "Generic Undefined Error";
                    error_message.msg_type = "Generic Undefined Error";
                    break;
            }
        }

        public static implicit operator string(Response v)
        {
            throw new NotImplementedException();
        }
    }
}
