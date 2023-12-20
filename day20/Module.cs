abstract class Module(string n, string[] d){

    public string Name {get;} = n;
    public string[] Destinations {get;} = d;


    abstract public List<Triple<string,string,bool>> Pulse(string from, bool high);

    abstract public void Reset();
}

class Broadcast(string n, string[] d) : Module(n, d){

    public override List<Triple<string,string,bool>> Pulse(string from, bool high){
        List<Triple<string,string,bool>> ret = [];
        foreach(string d in Destinations){
            ret.Add(new(Name, d, high));
        }
        return ret;
    }

    public override void Reset() {}
}

class FlipFlop(string n, string[] d) : Module(n, d){

    public bool status = false;

    public override List<Triple<string,string,bool>> Pulse(string from, bool high){
        if(Destinations.Equals(new String[]{""})){
            return [];
        }

        if(high){
            //nothing happens
            return [];
        }else{
            status = ! status;
            List<Triple<string,string,bool>> ret = [];
            foreach(string d in Destinations){
                ret.Add(new(Name, d, status));
            }
            return ret;
        }
    }

    public override void Reset() {
        status = false;
    }
}

class Conjuction(string n, string[] d) : Module(n, d){

    public Dictionary<string,bool> status = [];

    public void SetPredecessors(IEnumerable<string> pred){
        foreach(string s in pred){
            status.Add(s, false);
        }
    }

    public override List<Triple<string,string,bool>> Pulse(string from, bool high){
        status[from] = high;

        if(!status.Where(x => !x.Value).Any()){
            List<Triple<string,string,bool>> ret = [];
            foreach(string d in Destinations){
                ret.Add(new(Name, d, false));
            }
            return ret;
        }else{
           List<Triple<string,string,bool>> ret = [];
            foreach(string d in Destinations){
                ret.Add(new(Name, d, true));
            }
            return ret;
        }
    }

    public override void Reset() {
        foreach(string s in status.Keys){
            status[s] = false;
        }
    }
}

public class Triple<TA,TB,TC>(TA a, TB b, TC c){

    public TA A {get;} = a;
    public TB B {get;} = b;
    public TC C {get;} = c;
}