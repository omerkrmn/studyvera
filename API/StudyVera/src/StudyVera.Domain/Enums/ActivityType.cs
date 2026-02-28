﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Enums;

public enum ActivityType
{

    UserLogins=0,
    LessonCompleted =1, 
    LessonProgressed=2,
    SolvedAQuestion=3,
    ProfileUpdated=4,
    StudySessionCompleted=5,
    TopicReviewed=6,
    CreateMockExam=7,
}
