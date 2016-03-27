namespace CustomDataStructures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.ConstrainedExecution;

    public class Program
    {
        public static void Main(string[] args)
        {
            // var queue = new CustomQueue<int>();

            // queue.Enqueue(1);
            // queue.Enqueue(2);
            // queue.Enqueue(3);
            // queue.Enqueue(4);
            // queue.Enqueue(5);
            // queue.Enqueue(6);
               
            // Console.WriteLine("Six elements enqueued!");
            // foreach (var item in queue)
            // {
            //    Console.WriteLine(item);
            // }
               
            // Console.WriteLine("First element dequeued:{0}", queue.Dequeue());
            // Console.WriteLine("Second element dequeued:{0}", queue.Dequeue());
            // Console.WriteLine("Two elements dequeued!");
            // foreach (var item in queue)
            // {
            //     Console.WriteLine(item);
            // }
               
            // Console.WriteLine();

            // var customStack = new CustomStack<int>();
            // customStack.Push(1);
            // customStack.Push(2);
            // customStack.Push(3);
            // customStack.Push(4);
            // customStack.Push(5);
            // customStack.Push(6);
               
            // Console.WriteLine("Six elements pushed in a stack!");
            // foreach (var item in customStack)
            // {
            //     Console.WriteLine(item);
            // }
               
            // Console.WriteLine(customStack.Peek());
               
            // CustomList<int> lis = new CustomList<int>();
               
            // lis.Add(1);
            // lis.Add(2);
            // lis.Add(3);
            // lis.Add(4);
            // lis.Add(5);
            // lis.Add(6);
            // Console.WriteLine("Six elements added to a list!");
            // Console.WriteLine(lis);
            // lis.Remove(3);
            // Console.WriteLine("Elements at index 3 removed!");
            // Console.WriteLine(lis);
            Console.WriteLine("-----------------");
            Console.WriteLine("Custom linked list");

            CustomLinkedList<int> myLinkedList = new CustomLinkedList<int>();
            myLinkedList.Add(1);
            myLinkedList.Add(2);
            myLinkedList.Add(3);
            myLinkedList.Add(4);

            myLinkedList.Insert(0, 10);
            myLinkedList[2] = 20;

            foreach (var i in myLinkedList)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine(myLinkedList.Contains(20));
            
            Console.WriteLine(myLinkedList.Count);

            Console.WriteLine();
            Stack<int> stack = new Stack<int>();

            

        }
    }
}
