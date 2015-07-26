import json

tileSize = int(input("Enter tile size: "))
textureWidth = int(input("\nEnter texture width: "))
textureWidth = textureWidth // tileSize
print ("width =", textureWidth)
textureHeight = int(input("\nEnter texture height: "))
textureHeight = textureHeight // tileSize
print ("height =", textureHeight)
path = input("\nEnter a file path: ")


IDs = []
counter = 0

for y in range(0, textureHeight):
	for x in range(0, textureWidth):
                tile = {
                "ID" : counter,
                "X" : (x * tileSize),
                "Y" : (y * tileSize)
                }
                IDs.append(tile)
                counter += 1

with open(path, "w") as f:
        json.dump(IDs, f, indent=4)

