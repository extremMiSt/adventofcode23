using System.Text.RegularExpressions;

//task1
StreamReader reader = new StreamReader(File.OpenRead("./day01/input.txt")); 
Regex r = new Regex("[0-9]");

int sum = 0;

while(reader.Peek() >= 0){
    string? s = reader.ReadLine();
    if(s is not null){
        MatchCollection res = r.Matches(s);
        sum += int.Parse(res[0].Value+res[res.Count-1].Value);
    }
}
Console.WriteLine(sum);

//task2
StreamReader reader2 = new(File.OpenRead("./day01/input.txt")); 
Regex r2 = new("[1-9]");

int sum2 = 0;

while(reader2.Peek() >= 0){
    string? s = reader2.ReadLine();
    if(s is not null){
        MatchCollection matches = r2.Matches(replace(s));
        string n = matches[0].Value + matches[^1].Value;
        sum2 += int.Parse(n);
    }
}
Console.WriteLine(sum2);

static string replace(string s){
    s = s.Replace("one","one1one");
    s = s.Replace("two","two2two");
    s = s.Replace("three","three3three");
    s = s.Replace("four","four4four");
    s = s.Replace("five","five5five");
    s = s.Replace("six","six6six");
    s = s.Replace("seven","seven7seven");
    s = s.Replace("eight","seven8seven");
    s = s.Replace("nine","nine9nine");
    return s;
}