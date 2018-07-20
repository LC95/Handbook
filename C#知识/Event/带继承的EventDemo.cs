using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Helloworld
{

    class Program
    {
        static void Main(string[] args)
        {
            //Create the event publishers and subscriber
            Circle c1 = new Circle(54);
            Rectangle r1 = new Rectangle(12, 9);
            ShapeContainer sc = new ShapeContainer();

            // Add the shapes to the container.
            sc.AddShape(c1);
            sc.AddShape(r1);

            // Cause some events to be raised.
            c1.Update(57);
            r1.Update(7, 7);

            // Keep the console window open in debug mode.
            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
    public class ShapeEventArgs : EventArgs
    {
        private double newArea;
        public ShapeEventArgs(double d)
        {
            newArea = d;
        }
        public double NewArea
        {
            get => newArea;
        }
    }
    /// <summary>
    /// ��״��
    /// </summary>
    public abstract class Shape
    {
        protected double area;
        public double Area
        {
            get => area; set { area = value; }
        }
        //����һ��ί��, ���������¼��ʹ����¼�
        public EventHandler<ShapeEventArgs> ShapeChanged;
        public abstract void Draw();
        //����һ�������¼��ķ���
        protected virtual void OnShapeChanged(ShapeEventArgs e)
        {
            var handler = ShapeChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
    /// <summary>
    /// ����Բ����д����
    /// </summary>
    public class Circle : Shape
    {
        private double radius;
        public Circle(double d)
        {
            radius = d;
            area = 3.14 * radius * radius;
        }
        public void Update(double d)
        {
            radius = d;
            area = 3.14 * radius * radius;
            OnShapeChanged(new ShapeEventArgs(area));
        }
        protected override void OnShapeChanged(ShapeEventArgs e)
        {
            base.OnShapeChanged(e);
        }
        public override void Draw()
        {
            Console.WriteLine("Drawing circle");
        }
    }
    /// <summary>
    /// ������β���д����
    /// </summary>
    public class Rectangle : Shape
    {
        private double length;
        private double width;
        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
            area = length * width;
        }
        public void Update(double length, double width)
        {
            this.length = length;
            this.width = width;
            area = length * width;
            OnShapeChanged(new ShapeEventArgs(area));
        }
        protected override void OnShapeChanged(ShapeEventArgs e)
        {
            // Do any rectangle-specific processing here.

            // Call the base class event invocation method.
            base.OnShapeChanged(e);
        }
        public override void Draw()
        {
            Console.WriteLine("Drawing a rectangle");
        }

    }

    public class ShapeContainer
    {
        List<Shape> _list = new List<Shape>();

        public void AddShape(Shape s)
        {
            _list.Add(s);
            //Ϊ��״�ĸı��¼���Ӵ����¼�(����Ϊ��ӡһ������)
            s.ShapeChanged += HandleShapeChanged;
        }

        private void HandleShapeChanged(object sender, ShapeEventArgs e)
        {
            var s = sender as Shape;
            Console.WriteLine("Received event. Shape area is now {0}", e.NewArea);
            s?.Draw();
        }

    }
}
