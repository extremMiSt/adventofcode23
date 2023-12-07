using System.Globalization;

StreamReader reader = new StreamReader(File.OpenRead("./day07/input.txt")); 

List<Hand> hands = [];

while(reader.Peek() >= 0){
    string? s = reader.ReadLine();
    if(s is not null){
        string[] split = s.Split(" ");
        hands.Add(new(split[0], long.Parse(split[1])));
    }
}
hands.Sort(new Comp1());
long sum = 0;
for (int i = 0; i < hands.Count; i++){
    sum += hands[i].Bid * (i+1);
}
Console.WriteLine(sum);

hands.Sort(new Comp2());
long sum2 = 0;
for (int i = 0; i < hands.Count; i++){
    sum2 += hands[i].Bid * (i+1);
}
Console.WriteLine(sum2);