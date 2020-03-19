# Input Output. Delivery One

<img src="../docs/images/image-banner.png" align="middle" width="3000"/>

## G01: Victor Armisen and David Recuenco

## **Introduction**
The purpose of this notebook is explaining our work with the environment "Tennis" from the ml-agents examples.
This example, as its name says, simulates a tennis match between two agents which follows the real tennis rules.
What we wanted to do with the example was using only an agent and:
* Make the agent play paddle alone and following the game's rules: the ball has to touch the ground once before being hit and if the ball touches the ground two times in a row without touching the front wall, the point is lost.
* Make the agent do keepy-ups not letting the ball touching the ground. 
* The same keepy-ups as above but with wind, making it harder for the agent to keep the ball in the air.

### The team
Name | Enti email | Picture
--- | --- | ---
Victor Armisen | victorarmisencapo@enti.cat | placeholder picture
David Recuenco | davidrecuencooliver@enti.cat | placeholder picture


## Exercises 

The notebook will have the following sections:
1. **Introduction**
A short paragraph explaining what is the aim of this notebook, and a specification of the team involved (at least: picture, name, surname, email)

2. **Case analysis** 
You explain clearly how an example that is already implemented works. This includes at least an explanation of:
 a) what are the rewards, 
 b) what are the states, 
 c) how is, in general, the training implemented 

3. **Performance analysis** 
You do a performance analysis of the training using TensorBoard, exploring what parameters are most critical to a) make it work, b) produce smooth results, and c) train fast. In your analysis you should include a combination of parameters that does not work, and some that do work. You should also show some understanding of the role of the parameters, and try to explain why something works or doesn't.

4. **New case proposal** 
Taking as basis the example analysed, you propose, implement and test a different task for the same training procedure, or a very similar one. You explain how to train the new use case, and how the setup, rewards, states or training changed, compared to the example. 



## Grades

The grades will follow these guidelines:
1. Quality of documentation and class presentation (+1)
2. Case analysis (+3)
3. Performance analysis (+2)
4. New case proposal (+4)

## Extra requirements

* The notebook and code that you propose should be in folder delivery1, and work straight away from downloading the repository. 
* Your username should reflect your name+surname, and all your commits should be associated with your ENTI email
* Your delivery should have a tag labelled `ENTI_IO2020_D1_GXX`, where *XX* stands for your group number
* Your repository contributions should follow the git flow conventions. You should work on one or several  separate branches, and perform a merge  once satisfied with your contribution. The branch to which you will want to merge is called `develop-ENTI`
* The files should be organized preserving the structure already set up in the repository
* Your contributions should not collide with the ones from other development teams

**All the extra requirements should be satisfied for the submission to be evaluated.**


