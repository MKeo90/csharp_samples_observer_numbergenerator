using System;
using System.ComponentModel;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher die Zahlen auf der Konsole ausgibt.
    /// Diese Klasse dient als Basisklasse für komplexere Beobachter.
    /// </summary>
    public class BaseObserver : IObserver
    {
        #region Fields

        private readonly IObservable _numberGenerator;

        #endregion

        #region Properties

        public int CountOfNumbersReceived { get; private set; }
        public int CountOfNumbersToWaitFor { get; private set; }

        #endregion

        #region Constructors

        public BaseObserver(IObservable numberGenerator, int countOfNumbersToWaitFor)
        {
            if (numberGenerator == null)
            {
                throw new ArgumentNullException(nameof(numberGenerator));
            }
            _numberGenerator = numberGenerator;
            CountOfNumbersToWaitFor = countOfNumbersToWaitFor;
        }

        #endregion

        #region Methods

        #region IObserver Members

        /// <summary>
        /// Wird aufgerufen wenn der NumberGenerator eine neue Zahl generiert hat.
        /// </summary>
        /// <param name="number"></param>
        public virtual void OnNextNumber(int number)
        {
            CountOfNumbersReceived++;

            if (CountOfNumbersReceived >= CountOfNumbersToWaitFor)
            {
                DetachFromNumberGenerator();
            }
        }

        #endregion

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        protected void DetachFromNumberGenerator()
        {
            if (_numberGenerator.IsObserverRegistrated(this))
            {
                _numberGenerator.Detach(this);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void SetCounter()
        {
            CountOfNumbersReceived++;
        }

        #endregion
    }
}
