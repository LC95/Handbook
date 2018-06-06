转自SOF(https://stackoverflow.com/questions/9001603/why-does-0-tostring-return-an-empty-string-instead-of-0-00-or-at-least-0)

Because in a format string, the # is used to signify an optional character placeholder; it's only used if needed to represent the number.
If you do this instead: 0.ToString("0.##"); you get: 0
Interestingly, if you do this: 0.ToString("#.0#"); you get: .0
If you want all three digits: 0.ToString("0.00"); produces: 0.00