# ShoppingProgram
This is my first program in C# that serves to facilitate shopping. <br />

# Program funcionality: 
* Choosing the shop where we want to make shopping(Lidl,Dino,Auchan,Biedronka)
* Making our own backet, where we could save products which we want to buy, with their prices,kcal and grams.
* Automatically backet system, where we enter calories and days, and program recommend products for us.
* Smooth transition between manual and automatically mode. In both options, products added to our backet will be saved.
* Deleting products from backet.
* Showing a final price of our future shopping list.
# Future developments:
* Introduction of real data base, based on sql not on .csv file.
* Improve the quality of the automated system
* Generete a source code in .NET to make a application, not only program.
* Add a real prices(.csv file from https://pl.openfoodfacts.org/ was incomplete, and I had to simulate a values of price, calories and grams)
* Improve more advanced methods(It is my first program, and in future I want to go back to this program, and make it more transparent)
* After introduction of real data base, make categories like: dairy products, bakery products to introduct more user friendly interface.
# Introduction to automatically backet:
Firstly I have add to an excel 30 most popular healthy products recommended by nutritionists. Then I have counted a average number of calories per 100g. I noticed that product like olive oils, nuts overestimate a average number of calories. So I decided to investigate only prorducts which have more or equal calories than average(line 390 in program). After this decision I decided to split products to three categories: <br />
* First category - products which have less than 100 calories. This products should simulate vegetables and fruits. For example: apples, tomatoes, cucumbers,etc.
* Second category - products that complete our backet. They should have something beetwen 100kcals and 380kcals(average+deviation). This products should simulates something like youghurts,   cottage cheese, fish, turkey etc.
* Third category - products that are our 'bigger' things. The should simulate something like bread, pastas for one day or ready-made meals.
After a big research about a food and eating styles, I decided that 1/5 of our daily kcals should constitue products from first category. Per 1000kcal we should have I bigger meal, which should have something beetwen 500 and 700kcals(one portion). Rest of our kcals should be completed by second category products. It is my own system, I am not sure that it is good, but it is the best consensus I could come up with. I have add that this automatically backet only show us products with which we could obtain given number of calories. It is up to us whether we use it or not! <br />
# Visualisation of categories per calories:
![ss2](https://user-images.githubusercontent.com/105234376/180052255-96b04026-2aad-4c6b-ac11-aae696fefbf8.png)
