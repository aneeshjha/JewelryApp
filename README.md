# Tiny Jewelry Store

## Requirement

We require a small estimation application for a tiny Jewelry store. 
The application has the following abilities to cater the service

1. Login screen to login to the application. We have 2 categories of customers (**Regular** and **Privileged**)

2. Screen should have option to enter
   - **a. Gold price**
   - **b. Weight of the item**
   - **c. Discount percentage** (This option should be visible only for privileged customer and default
      discount is 2%. Please remember that store owner can apply any kind of discount plans in future but for now only 2% for privileged user)
      
3. The application should be able to print via these options
   - **a. On the screen (Show in a message box)**
   - **b. To a file (Store in a txt file)**
   - **c. To a paper via printer** (Option to be provided but implementation should be kept dummy
        which means method implementation should throw not implemented exception)

**Note:**
1. No need for any registration of user. By default, you can store two users one as Normal and
the other as Privileged user in any xml file. Need not use any database for doing so and just get the data from xml file during login process.
2. Discount is only applicable for Privileged user


**Expectations:**
- **You can use any kind of front end for accomplishing the task, which means front end can be replaceable.**
- **Please use best practices, principles, patterns everywhere.**
- **Please remember that the discount plan is not confined to only these, it is subjected to change anytime.**
- **Share the complete solution via email.**

**Below are the suggested screens for your reference (Feel free to adapt based on your frontend choice)**

### Login Screen

![](Login.JPG)

### Estimation Screen

![](estimation.JPG)
