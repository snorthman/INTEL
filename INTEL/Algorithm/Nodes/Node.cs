﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTEL
{
    abstract class Node
    {
        public enum Type { Input, Output, Hidden, Bias };

        public int ID { get; protected set; }
        public Type NodeType { get; protected set; }
        public virtual decimal Input { get; protected set; }
        public decimal Output { get; protected set; }

        public int ConnectionsCount { get { return _connections.Count; } }

        private List<Connection> _connections = new List<Connection>();

        public Node(int id)
        {
            ID = id;
        }

        public Connection Connect(Node target)
        {
            Connection c = new Connection(this, target);
            _connections.Add(c);
            return c;
        }

        public void Activation(Problem.ActivationFunction af)
        {
            Output = af(Input);
            Input = 0;
        }
        
        /// <summary>
        /// Each connected nodes Input gains this nodes Output * Weight of connection.
        /// </summary>
        public void Export()
        {
            foreach (Connection c in _connections)
                if (c.Enable)
                    c.To.Input += Output * c.Weight;
        }

        public override string ToString()
        {
            return ID + ": [" + NodeType + "] " + Input.ToString("F") + " > " + Output.ToString("F");
        }
    }
}
