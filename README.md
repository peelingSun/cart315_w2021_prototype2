Prototype 2: Momma Cub!

This is a management and training simulator with a virtual economy, that also tries to make playfulness out of programming concepts to get things done.

# Iteration 1 Design Journal

I've decided to address the issues of clarity raised in playtesters' feedback.
I mainly added tutorials for step-by-step instructions and revised it for clarity.
The player now starts at a logical point in the game: where they have to first buy a cub to see what it is they're playing with mainly.
The player starts with a single cub as suggested in the feedbacks. Then I walk the player step-by-step.
I've attempted to make the mini-game at the training centre clearer and refine its workings. Edit: The last concern ended up changing a lot of the design itself, as I felt that dragging cubs around wasn't the right direction. I think I would relay the satisfaction of dragging things to a more minor part of the game.

# Iteration 1 details

I had to find something I personally enjoyed (both as a developer and gamer). I've always had the lingo desire, so as I was struggling to correct or revise many of the problems related to the dragging mechanism, and especially how to make it worthwhile in a coherent gameplay, I realized that I didn't actually care about that level of detail anyways. The moment to moment design is something of a weakness, but also I wanted to test out ideas that took the feedbacks into consideration. For example, some reported that they wouldn't design gameplay around dragging the cubs, (and some said they loved it). I realized that I didn't actually know why I intended to do that in the first place, but that level of abstraction was merely to "get things done". 

What I got out of this last iteration was simply that I wanted to make mini-games but in a way that would connect to another level of abstraction in the business management "pipeline" : that is managing workers who would manage your assets. Right now, managing directly your cubs feels more like you're a farmer, so an independent worker. However, one of my main interests was to create an ambience where you're building this company with progressively non-materialistic goals. That is, to create some kind of 'family' warm ambience that would create this sense of zany in the player who is trying to make profits. The dissonance is what I'm after. Obviously, this scope would require a lot of time, so the main challenge for the final deadline is to make it simple enough with those ideas, so that I'm happy with the result. At a mimimum, the MVP would include the ideas that I would enjoy playing myself the most : managing some kind of economy or business as some kind of coach, while retaining the satisfaction of building satisfying relationships (...with NPCs). Reference comparisons would be a mix of Fire Emblem: Three Houses, and Harvest Moon. 

The second main interest was to make some kind of educative 'coding' game out of it. An accessible, playful educative game that would have what I find satisfying in it: writing high level commands/scripts. The idea came from attending a Python workshop where I was impressed by the host showing us an extremely high level of doing things in ML : "train(...args)". That was almost it. That would almost do everything. Geez! It left an impression in my soul. Now hopefully I can do something that I can be happy with within the next two weeks : otherwise I don't feel so bad in pursuing the project further (I know this is code for I'll never do it, but I just like these elements personally).

My iterative questions are in the issues, and they are mostly about how people felt about the tutorials and cli. Basically a lot of these changes are directly from the playtesting : people felt it was complicated and hard to understand so I made step by step instructions, with a customized way of pointing out the details that are important at every step (e.g., making a text label appear at the same time as an instruction from the Tutorial Girl that would make use of that was labelled by it, or making a building only clickable when the tutorial instruction says 'Click on the Training Centre now'.). To do that I started working on a customized conversation action system that would just have the stuffs I need (it parses what kind of instruction I want to give it. Other frameworks were too complicated to do what I needed only) on top of the CLI stuff. But now, it's all about making it to the finish line with a completed project. Another inspiration was simply the many code examples in object-oriented programming classes where westudy inheritance and polymorphism with farm animals and bank accounts but it's never applied in a meaningful way. The experience of living and making those academic instructions work in a real-time environment was an interesting idea to me.

# 1-week prototyping

Playtesters: Game prototype was frozen around 11:20 PM (last hotfix pushed) on February 24th. The versions before are obsolete.

The interest in this project was to create a hypercasual lighthearted themed game.
