This little snippet allows you to create a threaded update call that can work at maximum speed in monogame. Usefull for simulations.
Subclass it and implement the Update method.

Example Subclassing:

	class ExampleUpdate : AbstractAsynchronousUpdate
		{
			public int UpdateCalls { get; private set; }
			protected override void Update()
			{
				UpdateCalls++;
			}
		}

Example usage in monogame (an excerpt from an example Game class in monogame):

	ExampleUpdate update = new ExampleUpdate();
    protected override void Update(GameTime gameTime)
    {
        var keyboardstate = Keyboard.GetState(); //Get keyboard state
        if (keyboardstate.IsKeyDown(Keys.Space)) //If space, start the updating
        {
            update.StartThread();
        }
        if (keyboardstate.IsKeyDown(Keys.S))	//If S, stop the updating
        {
            update.StopThread();
        }
        update.Invoke();	//Invoke the update, gets ignored if the updating is running, does one update if not running.
    }