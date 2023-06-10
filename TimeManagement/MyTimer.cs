namespace   Backuper.TimeManagement
{
    public class MyTimer
    {
        public static System.Timers.Timer timer = null;
        /* cette methode dispose le timer s'il etat deja demaré sinon elle ne fait rien */
        public static void ReuinitialiseTimer()
        {
            if (timer != null)
            {
                timer.Dispose();
            }
            
        }
        /* cette methode retourne un nouveau timer qui suivre l'interval donnée en paramettre */
        public static System.Timers.Timer getTimer(int Interval)
        {
            timer=new System.Timers.Timer(Interval);
            return timer;
        }
    }
}
