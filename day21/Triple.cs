public class Triple<TA,TB,TC>(TA a, TB b, TC c){

    public TA A {get;} = a;
    public TB B {get;} = b;
    public TC C {get;} = c;
    
    public override bool Equals(object? obj){
        if(obj is null){
            return false;
        }
        return Equals(obj as Triple<TA,TB,TC>);
    }


    public override int GetHashCode(){
        return HashCode.Combine(A, B, C);
    }
}
