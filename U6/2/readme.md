# RSA Signaturen, Hash and Sign
a:  
Bei gleicher Nachricht ist beim wiederholten Signieren die Signatur gleich. Das Signieren ist deterministisch. Das ist so, weil die Hashfunktion SHA256 deterministisch ist.  
Diese gibt bei gleicher Nachricht den gleichen Hash zurueck. Vor allem ist die Laenge vom Hash immer gleich. Danach wird auf den Hashwert mit dem Exponenent d aus dem Private Key  
potenziert und mod N genommen. Die Berechnungen sind deterministisch. Bei gleicher Eingabe, gleichen Parametern ist die Ausgabe gleich.  
  
b:  
Die erhalten Signatur ist gueltig. Das sieht man im Output vom Program.  
  
c:  
Wenn die Nachricht modifiziert wird, ist die Signatur nicht mehr gueltig. Das sieht man auch im Programoutput.  
  
## Programoutput
Signatur 1: 0cOciET84hXV83Psu7zatVimAhgBPVE6WsvvMzDWLAhFm2rY2vxl8pXRQkvFhyjyrAr4FHWXluigbAWVcx2famZYSCgfdz26+kIJCySc6kT2iGBOX+0USL9rtTO4BIYdjHTq2K/coLej/fg9yZXDMDu833dECUV/SUF6eyQzDeM=  
  
Signatur 2: 0cOciET84hXV83Psu7zatVimAhgBPVE6WsvvMzDWLAhFm2rY2vxl8pXRQkvFhyjyrAr4FHWXluigbAWVcx2famZYSCgfdz26+kIJCySc6kT2iGBOX+0USL9rtTO4BIYdjHTq2K/coLej/fg9yZXDMDu833dECUV/SUF6eyQzDeM=  
  
  
Erfolgreich verifiziert.  
Es wird die Nachricht modifiziert...  
Es wird signiert und anschliessend verifiziert...  
Die Nachricht wurde modifiziert. Verifikation fehlgeschlagen...  
  