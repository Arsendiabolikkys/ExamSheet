﻿using System;

namespace ExamSheet.Repository.Faculty
{
    public class Faculty : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }
    }
}
