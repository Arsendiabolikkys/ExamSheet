using System;


namespace ExamSheet.Web
{
    public static class RatingMapper
    {
        public static string MapRatingToString(short rating)
        {
            if (rating >= 60 && rating < 65)
                return "E";
            else if (rating >= 65 && rating < 74)
                return "D";
            else if (rating >= 74 && rating < 82)
                return "C";
            else if (rating >= 82 && rating < 90)
                return "B";
            else if (rating >= 90 && rating <= 100)
                return "A";

            return "F";
        }

        public static string MapRatingToRange(short rating)
        {
            if (rating >= 0 && rating < 10)
                return "0-10";
            else if (rating >= 10 && rating < 20)
                return "10-20";
            else if (rating >= 20 && rating < 30)
                return "20-30";
            else if (rating >= 30 && rating < 40)
                return "30-40";
            else if (rating >= 40 && rating < 50)
                return "40-50";
            else if (rating >= 50 && rating < 60)
                return "50-60";
            else if (rating >= 60 && rating < 70)
                return "60-70";
            else if (rating >= 70 && rating < 80)
                return "70-80";
            else if (rating >= 80 && rating < 90)
                return "80-90";
            else
                return "90-100";
        }
    }
}
