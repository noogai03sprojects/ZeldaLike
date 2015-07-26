import json, sys

filePath = input ("enter path of ID file to read: ");

data = 0
f = open (filePath, "r")
data = json.load(f)
f.close();

run = True

while run:
	x = input("\nEnter X co-ord to find ID for: ")
	if x == "quit":
		run = False
		sys.exit()
	else:
		x = int(x)
		y = int(input("\nEnter Y co-ord to find ID for: "))
		#print(data)
		matchingX = [i for i in data if i["X"] == x]
		matchingY = [i for i in matchingX if i["Y"] == y]
		print ("ID: ", matchingY[0]["ID"])
		#for i in range()
