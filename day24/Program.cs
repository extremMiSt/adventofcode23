using DecimalSharp;
using Intersection = System.Tuple<DecimalSharp.BigDecimal, DecimalSharp.BigDecimal, DecimalSharp.BigDecimal, DecimalSharp.BigDecimal>;
using Line = System.Tuple<System.Tuple<DecimalSharp.BigDecimal, DecimalSharp.BigDecimal, DecimalSharp.BigDecimal>, System.Tuple<DecimalSharp.BigDecimal, DecimalSharp.BigDecimal, DecimalSharp.BigDecimal>>;

StreamReader reader = new(File.OpenRead("./day24/input.txt")); 
decimal boundMin = 7;
decimal boundMax = 27;

boundMin = 200000000000000;
boundMax = 400000000000000;

List<Line> lines = [];

while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        string[] tuples = s.Split(" @ ");
        string[] pos1 = tuples[0].Split(", ");
        string[] pos2 = tuples[1].Split(", ");
        lines.Add(
            new(new(new(pos1[0].Trim()),new(pos1[1].Trim()),new(pos1[2].Trim())),
                new(new(pos2[0].Trim()),new(pos2[1].Trim()),new(pos2[2].Trim())))
        );
    }
}

long count = 0;
for(int i = 0; i < lines.Count; i++){
    for(int j = i+1; j < lines.Count; j++){
        Intersection? tuple = cross(lines[i], lines[j]);
        if(tuple == null){
            continue;
        }
        if(tuple.Item1.GreaterThanOrEqualTo(0) && tuple.Item2.GreaterThanOrEqualTo(0)){
            if(tuple.Item3.GreaterThanOrEqualTo(boundMin) && tuple.Item3.LessThanOrEqualTo(boundMax) &&
               tuple.Item4.GreaterThanOrEqualTo(boundMin) && tuple.Item4.LessThanOrEqualTo(boundMax)){
                count++;
            }
        }
    }
}
Console.WriteLine("part1: " + count);

String path = "./day24/task2.smt";
if(File.Exists(path)){
    File.Delete(path);
}
StreamWriter writer = new(File.OpenWrite(path)); 
writer.WriteLine(@"(declare-const sx Int)
(declare-const sy Int)
(declare-const sz Int)

(declare-const svx Int)
(declare-const svy Int)
(declare-const svz Int)

(declare-const t1 Int)
(declare-const t2 Int)
(declare-const t3 Int)

(assert (= (+ sx (* t1 svx)) (+ " + lines[0].Item1.Item1 + @" (* t1 " + lines[0].Item2.Item1 + @"))))
(assert (= (+ sy (* t1 svy)) (+ " + lines[0].Item1.Item2 + @" (* t1 " + lines[0].Item2.Item2 + @"))))
(assert (= (+ sz (* t1 svz)) (+ " + lines[0].Item1.Item3 + @" (* t1 " + lines[0].Item2.Item3 + @"))))

(assert (= (+ sx (* t2 svx)) (+ " + lines[1].Item1.Item1 + @" (* t2 " + lines[1].Item2.Item1 + @"))))
(assert (= (+ sy (* t2 svy)) (+ " + lines[1].Item1.Item2 + @" (* t2 " + lines[1].Item2.Item2 + @"))))
(assert (= (+ sz (* t2 svz)) (+ " + lines[1].Item1.Item3 + @" (* t2 " + lines[1].Item2.Item3 + @"))))

(assert (= (+ sx (* t3 svx)) (+ " + lines[2].Item1.Item1 + @" (* t3 " + lines[2].Item2.Item1 + @"))))
(assert (= (+ sy (* t3 svy)) (+ " + lines[2].Item1.Item2 + @" (* t3 " + lines[2].Item2.Item2 + @"))))
(assert (= (+ sz (* t3 svz)) (+ " + lines[2].Item1.Item3 + @" (* t3 " + lines[2].Item2.Item3 + @"))))

(check-sat)
(get-model)");

writer.Flush();
writer.Close();

using(System.Diagnostics.Process pProcess = new System.Diagnostics.Process())
{
    pProcess.StartInfo.FileName = @"./day24/z3";
    pProcess.StartInfo.Arguments = "-smt2 "+path;
    pProcess.StartInfo.UseShellExecute = false;
    pProcess.StartInfo.RedirectStandardOutput = true;
    pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    pProcess.StartInfo.CreateNoWindow = true;
    pProcess.Start();
    string output = pProcess.StandardOutput.ReadToEnd();
    string[] olines = output.Split('\n');
    long sum = 0;
    for(int i = 0; i < olines.Length-1; i++){
        if(olines[i].Contains(" sx ") || olines[i].Contains(" sy ") || olines[i].Contains(" sz ")){
            sum +=long.Parse(olines[i+1].Replace(" ", "").Replace(")","").Replace("(",""));
        }
    }
    Console.WriteLine("part2: " + sum);
    pProcess.WaitForExit();
}


/*
Gleichungssystem:
vx1*t1 + x1  ==  vx2*t2 + x2
vy1      y2      vy2      y2

herleitung a,b:
vx1 * t1 + x1  ==  vx2*t2 + x2       | - x1
vx1 * t1       ==  vx2*t2 + x2 - x1  | / vx1
      t1       ==  (vx2/vx1)*t2 + (x2-x1)/vx1
=> a = (vx2/vx1)
   b = (x2-x1)/vx1

herleitung t2:
vy1*t1 + y1           ==  vy2*t2 + y2                  | ersetzen t1
vy1*(a*t2+b) + y1     ==  vy2*t2 + y2                  | ausmulitplizieren
vy1*a*t2 + vy1*b + y1 ==  vy2*t2 + y2                  | - (vy1*b +y1)
vy1*a*t2              ==  vy2*t2 + y2 - vy1*b - y1     | - vy2*t2
vy1*a*t2 - vy2*t2     ==  y2 - vy1*b - y1              | ausklammern
(vy1*a - xy2) * t2    ==  y2 - vy1*b - y1              | / (vy1*a - vy2)
t2                    == (y2 - vy1*b - y1) / (vy1*a - vy2)

herleitung t1:
t1  ==  a*t2 + b

herleitung schnittpunkt:
xx  =  vx1*t1 + x1;
xy  =  vy1*t1 + y1;
*/

static Intersection? cross(Line l1,Line l2){
    BigDecimal a = l2.Item2.Item1.DividedBy(l1.Item2.Item1);
    BigDecimal b = l2.Item1.Item1.Minus(l1.Item1.Item1).Div(l1.Item2.Item1);

    BigDecimal det = l1.Item2.Item2.Times(a).Minus(l2.Item2.Item2);
    if(det.Equals(0)){
        return null;
    }

    BigDecimal t2 = l2.Item1.Item2.Minus(l1.Item2.Item2.Times(b)).Minus(l1.Item1.Item2).DividedBy(det);
    BigDecimal t1 = a.Times(t2).Plus(b);

    BigDecimal xx = l1.Item2.Item1.Times(t1).Plus(l1.Item1.Item1);
    BigDecimal xy = l1.Item2.Item2.Times(t1).Plus(l1.Item1.Item2);

    return new(t1,t2,xx,xy);
}