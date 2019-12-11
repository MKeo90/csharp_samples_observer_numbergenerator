using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Implementiert einen Nummern-Generator, welcher Zufallszahlen generiert.
    /// Es können sich Beobachter registrieren, welche über eine neu generierte Zufallszahl benachrichtigt werden.
    /// Zwischen der Generierung der einzelnen Zufallsnzahlen erfolgt jeweils eine Pause.
    /// Die Generierung erfolgt so lange, solange Beobachter registriert sind.
    /// </summary>
    public class RandomNumberGenerator : IObservable
    {
        #region Constants

        private static readonly int DEFAULT_SEED = DateTime.Now.Millisecond;
        private const int DEFAULT_DELAY = 500;

        private const int RANDOM_MIN_VALUE = 1;
        private const int RANDOM_MAX_VALUE = 1000;

        #endregion

        #region Fields
        private List<IObserver> _observerList;
        private int _newNumber;

        #endregion

        #region Constructors
        
        /// <summary>
        /// Initialisiert eine neue Instanz eines NumberGenerator-Objekts
        /// </summary>
        public RandomNumberGenerator() : this(DEFAULT_DELAY, DEFAULT_SEED)
        {
            _observerList = new List<IObserver>();
        }

        /// <summary>
        /// Initialisiert eine neue Instanz eines NumberGenerator-Objekts
        /// </summary>
        /// <param name="delay">Enthält die Zeit zw. zwei Generierungen in Millisekunden.</param>
        public RandomNumberGenerator(int delay) : this(delay, DEFAULT_SEED)
        {
            _observerList = new List<IObserver>();
        }

        /// <summary>
        /// Initialisiert eine neue Instanz eines NumberGenerator-Objekts
        /// </summary>
        /// <param name="delay">Enthält die Zeit zw. zwei Generierungen in Millisekunden.</param>
        /// <param name="seed">Enthält die Initialisierung der Zufallszahlengenerierung.</param>
        public RandomNumberGenerator(int delay, int seed)
        {
            _observerList = new List<IObserver>();
        }

        #endregion

        #region Methods

        #region IObservable Members

        /// <summary>
        /// Fügt einen Beobachter hinzu.
        /// </summary>
        /// <param name="observer">Der Beobachter, welcher benachricht werden möchte.</param>
        public void Attach(IObserver observer)
        {
            if (_observerList.Contains(observer))
            {
                throw new InvalidOperationException();
            }
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }
            _observerList.Add(observer);
        }

        /// <summary>
        /// Entfernt einen Beobachter.
        /// </summary>
        /// <param name="observer">Der Beobachter, welcher nicht mehr benachrichtigt werden möchte</param>
        public void Detach(IObserver observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            if (IsObserverRegistrated(observer) == false)
            {
                throw new InvalidOperationException();
            }
            _observerList.Remove(observer);
        }

        /// <summary>
        /// Benachrichtigt die registrierten Beobachter, dass eine neue Zahl generiert wurde.
        /// </summary>
        /// <param name="number">Die generierte Zahl.</param>
        public void NotifyObservers(int number)
        {
            for (int i = 0; i < _observerList.Count; i++)
            {
                if (_observerList.Count <= 0)
                {
                    Console.WriteLine("Keine Observer mehr angemeldet => ENDE");
                    return;
                }
                _observerList[i].OnNextNumber(number);

                if (_observerList.Count <= 0)
                {
                    Console.WriteLine("Keine Observer mehr angemeldet => ENDE");
                    return;
                }
            }
        }

        #endregion

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Started the Nummer-Generierung.
        /// Diese läuft so lange, solange interessierte Beobachter registriert sind (=>Attach()).
        /// </summary>
        public void StartNumberGeneration()
        {
            // Zufallszahlengenerierung
            while(_observerList.Count > 0)
            {
                Random randomNumber = new Random();
                _newNumber = randomNumber.Next(RANDOM_MIN_VALUE, RANDOM_MAX_VALUE);
                Console.WriteLine($"Number Generator: Number generated: ' {_newNumber} ' ");

                // Benachrichtig die registrierten Observer
                NotifyObservers(_newNumber);
            }
        }

        // Hilfsmethode, welche überprüft ob der Observer noch angemeldet ist
        public bool IsObserverRegistrated(IObserver observer)
        {
            if (_observerList.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < _observerList.Count; i++)
            {
                if (_observerList[i].Equals(observer)) { return true; }
            }
            return false;
        }

        #endregion


    }

}
