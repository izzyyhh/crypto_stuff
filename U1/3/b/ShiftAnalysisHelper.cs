//this class is used for creating helper objects to analyze the frequencies of the letters
//a really simple score is returned
//the higher the score, the better the decryption has been
public class ShiftAnalysisHelper{
    public int freqE;
    public int freqSpace;
    public int shift;

    public ShiftAnalysisHelper(int freqE, int freqSpace, int shift){
        this.freqE = freqE;
        this.freqSpace = freqSpace;
        this.shift = shift;
    }

    public int Score(){
        //most simple score, i came up with, which sums up the occurrences of the letter E and Space, which are the most common in German Language
        return this.freqE + this.freqSpace; 
    }
}