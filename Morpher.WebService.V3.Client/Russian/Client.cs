﻿using System;
using System.Collections.Generic;

namespace Morpher.WebService.V3.Russian
{
    using System.Globalization;
    using System.Net;

    public class Client
    {
        readonly Func<MyWebClient> _newClient;

        internal Client(Func<MyWebClient> newClient)
        {
            _newClient = newClient;
            UserDict = new UserDict(_newClient);
        }

        public UserDict UserDict { get; }

        public DeclensionResult Parse(string lemma, DeclensionFlags? flags = null)
        {
            using (var client = _newClient())
            {
                if (flags != null)
                {
                    client.AddParam("flags", flags.ToString().Replace(" ", string.Empty));
                }

                client.AddParam("s", lemma);

                var declensionResult = client.GetObject<DeclensionResult>("/russian/declension");

                declensionResult.Nominative = lemma;

                return declensionResult;
            }
        }

        public IEnumerable<ResultOrError> Parse(
            IEnumerable<string> words,
            DeclensionFlags? flags = null)
        {
            using (var client = _newClient())
            {
                if (flags != null)
                {
                    client.AddParam("flags", flags.ToString().Replace(" ", string.Empty));
                }

                client.AddHeader(HttpRequestHeader.ContentType, "text/plain");
                return client.UploadString<IEnumerable<ResultOrError>>("/russian/declension",
                    string.Join("\n", words));
            }
        }

        public string AddStressMarks(string text)
        {
            using (var client = _newClient())
            {
                client.AddHeader(HttpRequestHeader.ContentType, "text/plain");
                return client.UploadString<string>("/russian/addstressmarks", text);
            }
        }

        public NumberSpellingResult Spell(decimal number, string unit)
        {
            using (var client = _newClient())
            {
                client.AddParam("n", number.ToString(new CultureInfo("en-US")));
                client.AddParam("unit", unit);

                return client.GetObject<NumberSpellingResult>("/russian/spell");
            }
        }

        public NumberSpellingResult SpellOrdinal(decimal number, string unit)
        {
            using (var client = _newClient())
            {
                client.AddParam("n", number.ToString(new CultureInfo("en-US")));
                client.AddParam("unit", unit);

                return client.GetObject<NumberSpellingResult>("/russian/spell-ordinal");
            }
        }

        public DateSpellingResult SpellDate(DateTime date)
        {
            using (var client = _newClient())
            {
                string dateString = date.ToString("ГГГГ-ММ-ДД", new CultureInfo("ru-RU"));
                client.AddParam("date", dateString);
                return client.GetObject<DateSpellingResult>("/russian/spell-date");
            }
        }

        public AdjectiveGenders AdjectiveGenders(string lemma)
        {
            using (var client = _newClient())
            {
                client.AddParam("s", lemma);

                return client.GetObject<AdjectiveGenders>("/russian/genders");
            }
        }

        public List<string> Adjectivize(string lemma)
        {
            using (var client = _newClient())
            {
                client.AddParam("s", lemma);

                return client.GetObject<List<string>>("/russian/adjectivize");
            }
        }
    }
}