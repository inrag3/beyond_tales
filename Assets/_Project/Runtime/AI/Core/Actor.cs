namespace _Project.Runtime.AI.Core
{
    public class Actor : IActor
    {
        private readonly IRule[] _rules;

        public Actor(params IRule[] rules) => 
            _rules = rules;
        public void Tick()
        {
            foreach (IRule rule in _rules)
            {
                if (!rule.IsExecutable) 
                    continue;
                rule.Execute();
                return;
            }
        }
    }
}