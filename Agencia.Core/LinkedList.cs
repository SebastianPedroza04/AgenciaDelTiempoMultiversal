namespace Agencia.Core
{
    public class LinkedListNode<T>
    {
        public T Value;
        public LinkedListNode<T> Next;

        public LinkedListNode(T value)
        {
            Value = value;
            Next = null;
        }
    }

    public class LinkedList<T>
    {
        public LinkedListNode<T> Head { get; private set; }
        public LinkedListNode<T> Tail { get; private set; }

        public void AddLast(T value)
        {
            var nodo = new LinkedListNode<T>(value);
            if (Head == null)
            {
                Head = Tail = nodo;
            }
            else
            {
                Tail.Next = nodo;
                Tail = nodo;
            }
        }

        public override string ToString()
        {
            var n = Head;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            while (n != null)
            {
                sb.AppendLine(n.Value?.ToString());
                n = n.Next;
            }
            return sb.ToString();
        }
    }
}
