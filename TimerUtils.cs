namespace CommunityPack
{
    public class TimerUtils
    {
        const int HOURS_TO_TICKS = 216000;
        const int MINS_TO_TICKS = 3600;
        const int SECS_TO_TICKS = 60;

        public static int Hours(float hours)
        {
            return (int)(HOURS_TO_TICKS * hours);
        }

        public static int Minutes(float mins)
        {
            return (int)(MINS_TO_TICKS * mins);
        }

        public static int Seconds(float seconds)
        {
            return (int)(SECS_TO_TICKS * seconds);
        }

        public static int Ticks(float hours, float mins, float seconds)
        {
            return (int)(HOURS_TO_TICKS * hours)
                + (int)(MINS_TO_TICKS * mins)
                + (int)(SECS_TO_TICKS * seconds);
        }
    }
}