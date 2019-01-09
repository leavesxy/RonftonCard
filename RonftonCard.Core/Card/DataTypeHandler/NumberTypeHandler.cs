using Bluemoon;
using Bluemoon.Broker;
using System;
using System.Collections.Generic;

namespace RonftonCard.Core.Card.DataTypeHandler
{
    public class NumberTypeHandler : ICardDataTypeHandler
    {
        private IDictionary<String, Type> numberType;

        public NumberTypeHandler()
        {
            this.numberType = new Dictionary<String, Type>()
            {
                { typeof(Int16).ToString(), typeof(Int16) },
                { typeof(UInt16).ToString(), typeof(UInt16) },
                { typeof(Int32).ToString(), typeof(Int32) },
                { typeof(UInt32).ToString(), typeof(UInt32) },
                { typeof(Int64).ToString(), typeof(Int64) },
                { typeof(UInt64).ToString(), typeof(UInt64) },
            };
        }

        public Object Parse(Type type, byte[] byteArray)
        {
            String func;

            if (type.Equals(typeof(Int16)))
                func = "ToInt16";
            else if (type.Equals(typeof(UInt16)))
                func = "ToUInt16";
            else if (type.Equals(typeof(Int32)))
                func = "ToInt32";
            else if (type.Equals(typeof(UInt32)))
                func = "ToUInt32";
            else if (type.Equals(typeof(UInt64)))
                func = "ToUInt64";
            else if (type.Equals(typeof(Int64)))
                func = "ToInt64";
            else
                return null;

            IMethodBroker broker = new MethodBroker(typeof(BitConverter), func);

            ResultArgs ret = broker.Invoke(new List<Object> { byteArray });

            if (ret.Succ)
                return ret.Result;

            return null;
        }

        public byte[] GetBytes(object obj, int length)
        {
            IMethodBroker broker = new MethodBroker(typeof(BitConverter), "GetBytes");

            Type type = numberType[obj.GetType().ToString()];

            ResultArgs ret = broker.Invoke(new List<Object> { obj });

            if (ret.Succ)
                return ret.Result as byte[];

            return ByteUtil.Malloc(length);
        }
    }
}