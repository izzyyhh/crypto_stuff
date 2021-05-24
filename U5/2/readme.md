leider bin ich an diesem beispiel gescheitert, aber vielleicht kriege ich dennoch punkte, wenn ich meinen weg erklaere
den generator zu brute forcen hat bei mir nicht funktioniert, da dies zu lange dauerte und bin deswegen gescheitert

# idee
zuerst wollte ich den generator brute forcen, indem ich mit 2 anfange und schau, ob ich mir jede Zahl aus der Gruppe mit dem generator bauen kann, sonst inkrementiere ich den generator und versuche es erneut. falls ich jede Zahl mit dem generator generiert habe, dann ist dies der kleinste generator, den Alice und Bob verwenden

Nachdem ich den generator brute forced habe, wuerde ich auch eine Tabelle mit den Zahlen der Gruppe mit dem generator haben, wo ich dann die Zahlen die Alice und Bob ausgetauscht haben, nachschauen koennte. Somit wuerde ich klein x und klein y aus der Tabelle bekommen und dann den gemeinsamen schluessel berechnen => g^(xy)
