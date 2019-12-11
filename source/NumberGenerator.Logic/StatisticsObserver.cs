using System;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher einfache Statistiken bereit stellt (Min, Max, Sum, Avg).
    /// </summary>
    public class StatisticsObserver : BaseObserver
    {
        #region Fields
        private int _counter;
        #endregion

        #region Properties

        /// <summary>
        /// Enthält das Minimum der generierten Zahlen.
        /// </summary>
        public int Min { get; private set; }

        /// <summary>
        /// Enthält das Maximum der generierten Zahlen.
        /// </summary>
        public int Max { get; private set; }

        /// <summary>
        /// Enthält die Summe der generierten Zahlen.
        /// </summary>
        public int Sum { get; private set; }

        /// <summary>
        /// Enthält den Durchschnitt der generierten Zahlen.
        /// </summary>
        public int Avg
        {
            get { return Sum / _counter; }
            private set { }
        }

        #endregion

        #region Constructors

        public StatisticsObserver(IObservable numberGenerator, int countOfNumbersToWaitFor) : base(numberGenerator, countOfNumbersToWaitFor)
        {
            if(numberGenerator == null)
            {
                throw new ArgumentNullException(nameof(numberGenerator));
            }
            _counter = 0;
            Min = 0;
            Max = 0;
            Sum = 0;
            Avg = 0;
            numberGenerator.Attach(this);
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return ($"BaseObserver [CountOfNumbersReceived='{CountOfNumbersReceived}', CountOfNumbersToWaitFor='{CountOfNumbersToWaitFor}'] => StatisticsObserver [Min='{Min}', Max='{Max}', Sum='{Sum}', Avg='{Avg}']");
        }

        // Neue, generierte Nummber wurde erhalten
        public override void OnNextNumber(int number)
        {
            _counter++;

            if (Min >= number)
            {
                Min = number;
            }

            if (Max < number)
            {
                Max = number;
            }

            Sum += number;
            Console.WriteLine($"CNumber: {CountOfNumbersToWaitFor}");

            if (_counter >= CountOfNumbersToWaitFor)
            {
                Console.WriteLine($"Statistics Observer: Received '{_counter}' of '{_counter}' => I am not interested in new numbers anymore => Detach()");
                DetachFromNumberGenerator();
            }
        }

        #endregion
    }
}
