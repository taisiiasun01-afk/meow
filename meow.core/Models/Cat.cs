namespace meow.core.Models
{
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public string Color { get; set; }

        public Cat() { }
        /// <summary>
        /// Сущность Кот
        /// </summary>
        /// <param name="id">номер</param>
        /// <param name="name">кличка</param>
        /// <param name="breed">порода</param>
        /// <param name="age">возраст</param>
        /// <param name="weight">вес</param>
        /// <param name="color">окрас</param>
        public Cat(int id, string name, string breed, int age, double weight, string color)
        {
            Id = id;
            Name = name;
            Breed = breed;
            Age = age;
            Weight = weight;
            Color = color;
        }
        /// <summary>
        /// для красивого вывода
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{Id}] {Name} ({Breed}), {Age} г., {Weight} кг, окрас: {Color}";
        }
    }
}
