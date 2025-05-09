﻿using Logic_Layer.cards;
using Logic_Layer.IA.Factories.IAFactories;

namespace Logic_Layer.IA.Factories
{
    /// <summary>
    /// Fabrique abstrate ddes IAs.
    /// </summary>
    public class IAFactory
    {
        private readonly Dictionary<string, ISpecificIAFactory> factories = new Dictionary<string, ISpecificIAFactory>()
        {
            { "DrunkedIA", new DrunkedIAFactory() },
        };

        /// <summary>
        /// Construit les IAs du type données en paramètre.
        /// </summary>
        /// <param name="parameters">Nom des IAs voulues.</param>
        /// <returns>Liste d'IAs de type demandé en paramètre.</returns>
        public List<IA> CreateIA(string[] parameters)
        {
            List<IA> res = new List<IA>();

            for (int i = 0; i < parameters.Length; i++)
            {
                // Si la fabrique est référencée
                if (this.factories.ContainsKey(parameters[i]))
                {
                    res.Add(this.factories[parameters[i]].CreateIA(i + 1, new Board(), new Hand(i + 1, new List<Card>())));
                }
            }

            return res;
        }
    }
}
