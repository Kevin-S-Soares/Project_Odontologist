using System.ComponentModel.DataAnnotations;

namespace Server.Models.Attributes
{
    public class DetailedTimeValidationAttribute : ValidationAttribute
    {
        private DetailedTime _model = default!;
        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            if (value is not DetailedTime)
            {
                ErrorMessage = "Invalid DetailedTime";
                return false;
            }
            _model = (DetailedTime)value;
            return ArePropertiesValid();
        }

        private bool ArePropertiesValid()
        {
            return IsStartDayOfTheWeekValid()
                && IsStartDateValid()
                && IsEndDayOfTheWeekValid()
                && IsEndDateValid();
        }


        private bool IsStartDayOfTheWeekValid()
        {
            int aux = Convert.ToInt32(_model.StartDayOfWeek);
            bool condition = aux >= 0 && aux <= 6;
            if (condition is false)
            {
                ErrorMessage = "Invalid DetailedTime.StartDayOfTheWeek";
            }
            return condition;
        }

        private bool IsEndDayOfTheWeekValid()
        {
            int aux = Convert.ToInt32(_model.EndDayOfWeek);
            bool condition = aux >= 0 && aux <= 6;
            if (condition is false)
            {
                ErrorMessage = "Invalid DetailedTime.EndDayOfTheWeek";
            }
            return condition;
        }

        private bool IsEndDateValid()
        {
            bool condition;
            try
            {
                var aux = new DateTime(_model.EndYear, _model.EndMonth, _model.EndDay);
                condition = aux.DayOfWeek == _model.EndDayOfWeek;
            } 
            catch (ArgumentOutOfRangeException)
            {
                condition = false;
            }
            
            if (condition is false)
            {
                ErrorMessage = "Invalid DetailedTime";
            }
            return condition;
        }

        private bool IsStartDateValid()
        {
            bool condition;
            try
            {
                var aux = new DateTime(_model.StartYear, _model.StartMonth, _model.StartDay);
                condition = aux.DayOfWeek == _model.StartDayOfWeek;
            } 
            catch (ArgumentOutOfRangeException)
            {
                condition = false;
            }
            
            if (condition is false)
            {
                ErrorMessage = "Invalid DetailedTime";
            }
            return condition;
        }
    }
}