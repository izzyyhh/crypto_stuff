using System;
using System.Collections;

public class LFSR {
    public bool[] register;
    private int[] taps = {2, 6, 12, 17};

    public LFSR(int[] seed, int bit = 60, int[] taps = null) {
        this.register = new bool[bit];
        if(taps != null) this.taps = taps;
        
        if(seed.Length != bit) throw new Exception("Seed must have length of register.");

        for(int i = 0; i < seed.Length; i++) {
            if(seed[i] == 1) register[i] = true;
        }
    }

    public bool Step() {
        bool rInput = true;
        try {

            foreach(int tap in taps) {
                rInput ^= register[tap];
            }
            
            // least significant bit
            bool rOutput = register[register.Length - 1];
            
            bool[] registerClone = (bool[])register.Clone();

            for(int i = 1; i < register.Length; i++) {
                register[i] =  registerClone[i - 1];
            }
            
            // most significant bit
            register[0] = rInput;

            return rOutput;

        } catch {
            throw new Exception("Given tap is outside of registers' length");
        }
    }

    // Aufzeichnung vom Strom mit Laenge 'steps'
    public int[] Steps(int steps) {
        int[] outputs = new int[steps];

        for(int i = 0; i < 512; i++) {
            this.Step();
        }

        for(int i = 0; i < steps; i++) {
            outputs[i] = this.Step() ? 1 : 0;
        }

        return outputs;
    }
}