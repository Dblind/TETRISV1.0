namespace TETRISV1
{
    // abstract class Figures
    // {
    //     abstract public string name { get; set; }

    //     //abstract public char[,] Form;
    // }

    // class Squire : Figures
    // {
    //     override public string name { get { return name; } set { value = "Squire"; } }

    //     //override public char[,] Form;//{ get; set; }
    //     // {
    //     //     get { return Form; }
    //     //     set
    //     //     {
    //     //         char[,] a = {
    //     //             {'+', '+'},
    //     //             {'+', '+'}
    //     //             };
    //     //         value = a;
    //     //     }
    //     // }

    //     //public Squire()
    //     // {
    //     //     Form = new char[,] {
    //     //             {'+', '+'},
    //     //             {'+', '+'} };


    //     // }
    // }

    class LineSizeTwo// : Figures
    {
        public string name { get; set; }

        public char[,] Form;
        public LineSizeTwo()
        {
            Form = new char[,] {
                    {Move.keyBuild},
                    {Move.keyBuild} };
        }
    }
    class Angle
    {
        public char[,] Form;
        public Angle()
        {
            Form = new char[,] {
                {' ', Move.keyBuild,' '},
                {Move.keyBuild, Move.keyBuild,Move.keyBuild} };
        }
    }
}
