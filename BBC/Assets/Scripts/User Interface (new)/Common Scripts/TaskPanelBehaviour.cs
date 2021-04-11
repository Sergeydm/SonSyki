﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaskPanelBehaviour : MonoBehaviour
{
    public int taskNumber;
    public bool isNextTaskButtonAvailable;
    private int sceneIndex;
    private GameObject canvas;
    private GameObject startButton;
    private GameObject UICollector;
    private Button nextTaskButton;
    private Text currentTaskTitle;
    private Text currentTaskDescription;
    private Text currentExtendedTaskTitle;
    private Text currentExtendedTaskDescription;
    private InputField codeField;
    private InputField resultField;
    private InputField outputField;
    private Button nextLevelButton;
    private PadBehaviour pad;
    private List<string> taskTitles = new List<string>();
    private List<string> taskDescriptions = new List<string>();
    private List<string> taskExtendedDescriptions = new List<string>();
    private List<string> taskStartCodes = new List<string>();

    public void ChangeTask()
    {
        if (taskNumber <= taskTitles.Count)
        {
            currentTaskTitle.text = taskTitles[taskNumber - 1];
            currentExtendedTaskTitle.text = taskTitles[taskNumber - 1];
            currentTaskDescription.text = taskDescriptions[taskNumber - 1];
            currentExtendedTaskDescription.text = taskExtendedDescriptions[taskNumber - 1];
            pad.startCode = taskStartCodes[taskNumber - 1];
            codeField.text = pad.startCode;
            resultField.text = "";
            outputField.text = "";
            canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription();
            isNextTaskButtonAvailable = false;
            startButton.GetComponent<StartButtonBehaviour>().taskNumber = taskNumber;
        }
        else
        {
            GameObject.Find("CloseExtendedTaskButton").SetActive(false);
            nextLevelButton.gameObject.SetActive(true);
            canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription();
            switch(sceneIndex)
            {
                case 1:
                    currentExtendedTaskTitle.text = "Поздравляем!";
                    currentExtendedTaskDescription.text = "     Вы прекрасно освоили операции с числовыми данными, а ваше приключение только начинается.\n" +
                                                          "     В путь!";
                    break;
                case 2:
                    currentExtendedTaskTitle.text = "Поздравляем!";
                    currentExtendedTaskDescription.text = "     Вы смогли пройти этот запутанный лес-лабиринт, а заодно получили навыки работы с условиями.\n" +
                                                          "     Не будем терять здесь больше времени и двинемся дальше!";
                    break;
            }                  
        }
    }

    public void CloseTask()
    {
        pad.transform.position = UICollector.transform.position;
        GameObject.Find("TaskPanel").transform.position = UICollector.transform.position;
        GameObject.Find("TaskCamera_" + taskNumber).GetComponent<Camera>().enabled = false;
        canvas.GetComponent<GameData>().currentSceneCamera.enabled = true;
    }

    public void ShowIntroduction_Level_1()
    {
        currentExtendedTaskTitle.text = "Новые горизонты";
        currentExtendedTaskDescription.text = "     Итак, наше путешествие начинается!\n" +
                                              "     Пока не происходит ничего интересного. Можно спокойно полюбоваться природой... и продолжить осваивать программирование!\n" +
                                              "     Раз уж робот больше не заперт с нами в четырёх стенах, можно наконец-то поуправлять им. Нажимай клавиши <b><color=green>WASD</color></b> для передвижения и поворота. Когда появится что-нибудь интересное (например, задание), внизу появится подсказка." +
                                              "Нажми на нёё, и сможешь узнать что-то новое.\n" +
                                              "     Ну что ж, полный вперёд!";
    }

    public void ShowIntroduction_Level_2()
    {
        currentExtendedTaskTitle.text = "Куда ведёт тропа?";
        currentExtendedTaskDescription.text = "     Двигаясь по тропинке, робот (а вместе с ним и мы) попали в лес. Как бы в нём не заблудиться...\n" +
                                              "     Наш железный друг знает, где мы должны выйти, но ориентироваться на местности его программа, похоже, не умеет. Без нашей помощи здесь не обойтись.\n" +
                                              "     Судя по всему, важные места здесь помечены <b><color=green>прямоугольными табличками</color></b>. Если увидите такие, обязательно загляните, наверняка мы сможем узнать что-нибудь полезное.";
    }

    public void ShowIntroduction_Level_3()
    {

    }

    public void ShowIntroduction_Level_4()
    {

    }

    public void ShowIntroduction_Level_5()
    {

    }

    private void FormTasks_Level_1()
    {
        taskTitles.Add("Раз ромашка, два ромашка...");
        taskDescriptions.Add("  - Создай переменную redFlowersCount и запиши в неё количество красных цветков\n" +
                             "  - Создай переменную yellowFlowersCount и запиши в неё количество жёлтых цветков\n" +
                             "  - Верни сумму всех цветков на лужайке");
        taskExtendedDescriptions.Add("     Итак, вспомним, на чём мы остановились.\n" +
                                     "     В последней задаче в конце мы записали несколько переменных через знак \"+\" и получили их сумму. Да, мы можем выполнять <b><color=green>арифметические</color></b> (и многие другие) операции не только с числами, но и с <b><color=green>переменными</color></b>, " +
                                     "которые их содержат! Это одна из базовых функций любого языка " +
                                     "программирования.\n" +
                                     "     Познакомимся с простой арифметикой, и для начала - со сложением, хотя вы уже и знакомы с ним. Поработаем с ним ещё разок!\n" +
                                     "     На лужайке рядом с домом растёт много всего. Видите цветы? Посчитаем, сколько их тут растёт.");
        taskStartCodes.Add("public int Execute()\n" +
                           "{\n" +
                           "\n" +
                           "\n" +
                           "}");

        taskTitles.Add("Геометрия на грибах");
        taskDescriptions.Add("  - Создайте переменную volume и в неё формулу для расчёта объёма. Формула для объёма полусферы:\n" +
                             "     V = (2/3) * pi * R^3\n" +
                             "  - В конце верните значение объёма");
        taskExtendedDescriptions.Add("     Отлично! Какие же ещё операции нам доступны?\n" +
                                     "     Конечно, остальные простейшие операции: <b><color=green>вычитание</color></b> (знак -), <b><color=green>умножение</color></b> (знак *) и <b><color=green>деление</color></b> (знак /). Знак ^ для возведения в степень здесь не работает, но его можно заменить " +
                                     "умножением числа на само себя. Используем эти знания в следующем задании!\n" +
                                     "     Путешествие начинается, конечно, очень скучно: ни сокровищ, ни опасностей, а вокруг только деревья да камни. Займём себя чем-нибудь и посчитаем... ну, например,... объём шляпки гриба! Да, а почему бы и нет)? Вообще-то, тут геометрию вспомнить надо, целая наука, это вам не " +
                                     "овец считать перед сном! Заодно посмотрим, как работают остальные арифметические операции.");
        taskStartCodes.Add("public double Execute()\n" +
                           "{\n" +
                           "    double pi = 3.14;\n" +
                           "    double R = 0.02;\n" +
                           "}");

        taskTitles.Add("Всё должно быть поровну");
        taskDescriptions.Add("  - Запишите в переменную flowersCount количество растущих цветков" +
                             "  - В конце верните остаток от их деления на 2");
        taskExtendedDescriptions.Add("     Красивые цветы! Вот бы нарвать их и подарить родителям и друзьям! Интересно, хватит ли их всем...\n" +
                                     "     Кстати, мы совсем забыли рассказать вам об ещё одном операции, которая тоже является очень важной - получение <b><color=green>остатка от деления</color></b> (обозначается знаком процента - %). Она может и не встречается в программах так часто, " +
                                     "но это не отменяет её важности. Порой без неё просто не обойтись. Как и нам с нами в следующем задании.\n" +
                                     "     Узнаем, получится ли разделить и подарить равное количество цветов маме и бабушке или останется лишний?");
        taskStartCodes.Add("public int Execute()\n" +
                           "{\n" +
                           "\n" +
                           "\n" +
                           "}");

        taskTitles.Add("Одним больше, одним меньше");
        taskDescriptions.Add("  - Увеличьте количество камней на 1, используя инкремент\n" +
                             "  - Ниже увеличьте их число втрое, используя сокращённую запись умножения" +
                             "  - В конце верните количество камней");
        taskExtendedDescriptions.Add("     Пока мы с вами считаем цветочки, робот всю дорогу считает камни, даже самые маленькие, которые нам, может, и не видны вовсе. Давайте и мы поучаствуем! Только сначала - небольшой секрет.\n" +
                                     "     В языке C#, помимо обычных арифметических операций, есть ещё и <b><color=green>сокращённые</color></b>. Они позволяют выполнять те же действия и при этом писать меньше кода. Например,\n" +
                                     "     number += 3;  //То же самое, что number = number + 3;\n" +
                                     "     number *= 2;  //То же самое, что number = number * 2;\n" +
                                     "     number++;     //Увеличивает значение на 1\n" +
                                     "     number--;     //Уменьшает значение на 1\n" +
                                     "     Сокращённую запись можно применять ко всем операторам (+, -, *, /, %). Предлагаем и вам попробовать её использовать в следующем задании. Посчитаем камни.");
        taskStartCodes.Add("public int Execute()\n" +
                           "{\n" +
                           "    int rocksCount = 99;\n" +
                           "\n" +
                           "}");
    }

    private void FormTasks_Level_2()
    {
        taskTitles.Add("Первая развилка");
        taskDescriptions.Add("  - Напиши условную конструкцию if: if (isRightPointerHere) {...}\n" +
                             "  - Внутри (в фигурных скобках) верни true, если условие истинно.");
        taskExtendedDescriptions.Add("     Ну вот и первая загвоздка: куда идти? Вроде бы указатели есть, да только слова на них затёрлись и ни одной буквы не разобрать. Что ж, будем придерживаться такой стратегии: чтобы не потеряться, на каждой развилке будем идти вправо.\n" +
                                     "     Теперь нужно, чтобы робот смог понять, куда надо двигаться. Мы должны сказать ему: \"Иди вправо, если туда ведёт указатель.\" Как сделать это с помощью кода?\n" +
                                     "     На помощь нам придут специальные <b><color=green>условные конструкции</color></b>, которые выполняют определённые действия в зависимости от того, выполнено некое <b><color=green>условие</color></b> или нет. Такие конструкции используют" +
                                     "специальный тип данных <b><color=green>bool</color></b>, принимающий значения <b><color=green>true</color></b> (истина) и <b><color=green>false</color></b> (ложь). Этот тип могут иметь как переменные, так и некоторые выражения.\n" +
                                     "     Итак, самой важной конструкцией является <b><color=green>if</color></b> (\"если\"). Принцип её работы: если некоторое условие истинно (имеет значение true), то программа выполняет некоторые инструкции.\n" +
                                     "       if (/* условие */)\n" +
                                     "       {\n" +
                                     "          /*\n" +
                                     "             Блок кода\n" +
                                     "          /*\n" +
                                     "       }\n" +
                                     "     Её мы и используем для решения первой задачи: нам нужно сказать роботу идти вправо, если есть указатель в ту сторону. Зрительные анализаторы робота определят наличие указателя и передадут информацию как <b><color=green>аргумент</color></b> " +
                                     "метода.");
        taskStartCodes.Add("public bool Execute(bool isRightPointerHere)\n" +
                           "{\n" +
                           "    \n" +
                           "    \n" +
                           "}\n");

        taskTitles.Add("Что здесь росло?");
        taskDescriptions.Add("  - Напиши условие, используя if: если количество бревён logsCount больше либо равно 8, верни первый тип дерева (1)\n" +
                             "  - А затем второе условие: если количество бревён logsCount меньше 8, верни второй тип дерева (2)\n");
        taskExtendedDescriptions.Add("     Кажется, стратегия \"идти всегда вправо\" быстро дала сбой - мы пришли в тупик. Но раз уж мы пришли сюда, немного потренируемся использовать условия.\n" +
                                     "     Как мы уже говорили, в условие для if мы можем записать не только переменные типа bool, но и некоторые выражения. Самые распространённые и очевидные - это <b><color=green>операции сравнения</color></b> чисел. Это ==, >, <, <=, >=, !=.\n" +
                                     "       1 == 1   // 1 равно 1, выражение равно true\n" +
                                     "       5 > 3    // 5 больше 3, выражение равно true\n" +
                                     "       4 <= 2   // 4 меньше или равно 2, выражение равно false\n" +
                                     "       number != 9   // number не равно 9, значение выражения зависит от значения number\n" +
                                     "     В конструкции if они используются также, как и переменные типа bool:\n" +
                                     "       if (number > 10)\n" +
                                     "           return true;\n" +
                                     "     В последнем примере мы записали конструкцию if без фигурных скобок. И так можно делать, но только если блок кода после условия занимает одну строчку. В остальных случаях их наличие обязательно.\n" +
                                     "     Теперь решим задачку. Мы с вами зашли в тупик, но похоже здесь излюбленное место дровосеков: много пеньков, лежащие кучами брёвна... Брёвна на вид одинаковые, только количество отличается. Узнаем, какое дерево было на месте каждого пенька." +
                                     "Если брёвен очень много, то скорее всего, это высокая сосна, а если не так много, то это дуб. Для простоты обозначим сосну типом дерева 1, а дуб - типом дерева 2.\n");
        taskStartCodes.Add("public int Execute(int logsCount)\n" +
                           "{\n" +
                           "    \n" +
                           "    \n" +
                           "}\n");

        taskTitles.Add("Новая стратегия");
        taskDescriptions.Add("  - Напиши условие: если указатель налево есть, верни номер этого пути (1), иначе верни номер другого пути (2)");
        taskExtendedDescriptions.Add("     Прошлая наша стратегия не сработала. Попробуем другую: на каждой развилке будем сворачивать налево, если туда ведёт указатель, иначе пойдём направо.\n" +
                                     "     Очевидно, чтобы выбрать маршрут, нужно проверить два условия. Но если первое выполнится, то нам не нужно проверять второе, однако программа это сделает, что не очень эффективно. Как это исправить?\n" +
                                     "     Для таких случаев существует конструкция <b><color=green>else</color></b>, которая выполняет определённые инструкции только тогда, когда условие, переданное в if, ложно.\n" +
                                     "       if (/* условие */)\n" +
                                     "       {\n" +
                                     "          /*\n" +
                                     "             Блок кода\n" +
                                     "          /*\n" +
                                     "       }\n" +
                                     "       else\n" +
                                     "       {\n" +
                                     "          /*\n" +
                                     "             Блок кода\n" +
                                     "          /*\n" +
                                     "       }\n" +
                                     "     Эта конструкция <b><color=green>if-else</color></b> весьма полезна и часто используется в программах. Самое время и нам её применить. Но напоследок - небольшое замечание.\n" +
                                     "     Если в блоке if вы используете return, то писать else НЕ нужно, ведь return будет означать конец выполнения метода, а значит в другую ветку условий программа не пойдёт. Помните об этом!");
        taskStartCodes.Add("public int Execute(bool isLeftPointerHere)\n" +
                           "{\n" +
                           "    \n" +
                           "    \n" +
                           "}\n");

        taskTitles.Add("Съедобно-несъедобно");
        taskDescriptions.Add("  - Напиши условие: если гриб имеет высоту больше 6, либо высоту меньше 4 и имеет пятна (isMushroomSpotted) и цвет шляпки красный (isMushroomRed), верни false\n" +
                             "  - В остальных случаях возвращай true");
        taskExtendedDescriptions.Add("     Да что ж такое-то! Первый раз свернули налево - а тут завал. Придётся снова возвращаться. Но чтобы время не прошло даром, расскажем вам ещё кое-что об условиях.\n" +
                                     "     Взгляните на эти грибы. Вот какие, по-вашему мнению, съедобны? Мы вот, кроме мухоморов, ничего не знаем, а робот наш в этом деле и вовсе дилетант. Попробуем распознать мухомор.\n" +
                                     "     У него два главных атрибута - красная шляпка и белые пятнышки на ней. Выходит, есть два условия. Как их проверить одновременно?\n" +
                                     "     Можно вложить один if в другой:\n" +
                                     "       if (/* условие 1*/)\n" +
                                     "       {\n" +
                                     "          if (/* условие 2*/)\n" +
                                     "          {\n" +
                                     "              /*\n" +
                                     "                 Блок кода\n" +
                                     "              /*\n" +
                                     "          }\n" +
                                     "       }\n" +
                                     "     Полученная запись может использоваться, но она довольно громоздкая. К тому же, для нашего случая можно использовать кое-что получше, а именно - <b><color=green>логические операторы</color></b> && (логическое \"И\") " +
                                     "и || (логическое \"ИЛИ\"). Они позволяют объединять несколько условий в одном if-е в самых различных комбинациях.\n" +
                                     "       if (number > 10 && number < 20)\n" +
                                     "       {\n" +
                                     "          /*\n" +
                                     "             Этот код выполнится, если number\n" +
                                     "             будет больше 10, но меньше 20\n" +
                                     "          /*\n" +
                                     "       }\n" +
                                     "     Зная это, мы можем определить (ну или хотя бы попытаться), какие грибы съедобные. Опираясь на наши феноменальные (нет) познания, будем считать несъедобными мухоморы и грибы-гиганты (ну мало ли что случится).");
        taskStartCodes.Add("public bool Execute(int mushroomHeight, bool isMushroomSpotted, bool isMushroomRed)\n" +
                           "{\n" +
                           "    \n" +
                           "    \n" +
                           "}\n");

        taskTitles.Add("Правильный выбор");
        taskDescriptions.Add("  - Напиши условие: если первый путь свободен, а второй и третий - нет, верни его номер - 1\n" +
                             "  - Иначе, если второй путь свободен, а первый и третий - нет, верни его номер - 2\n" +
                             "  - В противном случае верни номер третьего пути");
        taskExtendedDescriptions.Add("     Всё! Довольно! Хватит с нас этих стратегий. Включим наш прагматизм и будем выбирать тот путь, на котором нет преград. Судя по нашему опыту, он тут везде один.\n" +
                                     "     Мы уже многое знаем об условиях, и в задаче поиска пути нам пригодится недавно изученный else. Но если путей будет больше двух, как тогда быть? Писать неэффективный набор if-ов?\n" +
                                     "     На самом деле, можно использовать ещё одну крайне полезную конструкцию - <b><color=green>else if</color></b>. Она позволяет выполнять другой набор действий, если условие в if не выполнилось, но условие в else if выполнилось." +
                                     "Проще проиллюстрировать это на примере:\n" +
                                     "       if (/* условие 1 */)\n" +
                                     "       {\n" +
                                     "          /*\n" +
                                     "             Блок кода. Выполнится, если условие 1 истинно\n" +
                                     "          /*\n" +
                                     "       }\n" +
                                     "       else if (/* условие 2 */)\n" +
                                     "       {\n" +
                                     "          /*\n" +
                                     "             Блок кода. Выполнится, если условие 1 ложно, а условие 2 истинно\n" +
                                     "          /*\n" +
                                     "       }\n" +
                                     "       else\n" +
                                     "       {\n" +
                                     "          /*\n" +
                                     "             Блок кода. Выполнится, если оба условия ложны.\n" +
                                     "          /*\n" +
                                     "       }\n" +
                                     "     Таких else if можно использовать сколь угодно много, что даёт возможность заставлять программу работать по-разному в зависимости от разных ситуаций. Это нам на руку: теперь, наконец, можем найти правильный путь.\n" +
                                     "     Чуть не забыли! В прошлый раз, говоря об операторах, мы забыли упомянуть <b><color=green>отрицание</color></b>. Оно обозначается ! перед выражением или булевой (типа bool) переменной и меняет их значение на противоположное.\n" +
                                     "     Например, !(10 == 10) благодаря отрицанию будет иметь значение false, хотя выражение внутри скобок истинно. Это знание нам тоже пригодится.");
        taskStartCodes.Add("public int Execute(bool isFirstPathClear, bool isSecondPathClear, bool isThirdPathClear)\n" +
                           "{\n" +
                           "    \n" +
                           "    \n" +
                           "}\n");

        taskTitles.Add("Наводим мосты");
        taskDescriptions.Add("  - Используя условия, возвращай true, если длина бревна больше или равна 3, но меньше 5\n" +
                             "  - Иначе возвращай false");
        taskExtendedDescriptions.Add("     Кажется, мы рассказали вам всё самое основное и важное про условия. Пора применить все наши познания на практике, и перед нами как раз очень серьёзная проблема - моста через реку нет! Видимо, его унесло течением, и на другой берег теперь" +
                                     "никак не добраться. Но, похоже, удача нас ещё не покинула: тут лежат брёвна и довольно большие, так что можно попробовать соорудить мост.\n" +
                                     "     Поможем роботу подобрать нужные брёвна: достаточно длинные, чтобы достать до берега, но не слишком, чтобы он смог их поднять.");
        taskStartCodes.Add("public bool Execute(int logLength)\n" +
                           "{\n" +
                           "    \n" +
                           "    \n" +
                           "}\n");

        taskTitles.Add("Тайный знак");
        taskDescriptions.Add("  - С помощью условий напиши программу, возвращающую номер пути:\n" +
                             "  - 1, если количество камней в башенке равно двум,\n" +
                             "  - 2, если количество камней в башенке равно шести,\n" +
                             "  - 3, если количество камней в башенке равно четырём\n" +
                             "  - В остальных случаях будем идти по первому пути (верни единицу)");
        taskExtendedDescriptions.Add("     И вновь развилка. И пути все вроде свободны. А слова на указателях нечитаемы. Что теперь делать?\n" +
                                     "     Похоже, выход всё же есть. На примерной карте, имеющейся у робота, некоторые пути обозначены башенками из камней разной высоты. Похоже, это тайный знак для таких же искателей сокровищ, как мы.\n" +
                                     "     Итак, ориентируясь на высоту башенок из камней, выберем правильный путь. Условия, как и всегда, нам помогут. Если что-то пойдёт не так, выберем путь интуитивно, ведь что ещё можно сделать в такой ситуации?");
        taskStartCodes.Add("public int Execute(int rocksCount)\n" +
                           "{\n" +
                           "    \n" +
                           "    \n" +
                           "}\n");

        taskTitles.Add("Столько тайн, столько загадок...");
        taskDescriptions.Add("  - С помощью условий напиши программу, возвращающую номер пути:\n" +
                             "  - 1, если тайный символ (secretSymbol) - цифра 3,\n" +
                             "  - 2, если тайный символ - цифра 5,\n" +
                             "  - 3, если тайный символ - цифра 7\n" +
                             "  - 4 в остальных случаях");
        taskExtendedDescriptions.Add("     Ох уж эти загадки! Когда выход из леса так близок, они снова решили появиться. Надеемся, в последний раз...\n" +
                                     "     Кажется, эти камни на тропинках образуют некие числа, но почему-то какие-то странные. Очевидно, это ещё один шифр.\n" +
                                     "     И правда, на карте они также странно обозначены. Что ж, осталось найти соответствие и продолжить путешествие. Но прежде - небольшой секрет.\n" +
                                     "     На самом деле, мы рассказали вам ещё не о всех условных конструкциях, но постараемся сделать это немного позже. Но об одной расскажем сейчас - это конструкция <b><color=green>switch</color></b>\n" +
                                     "     Она по своей сути напоминает сочетание if и else if, но позволяет писать более лаконичный и красивый код. Покажем это на примере:\n" +
                                     "       int number = 2;\n" +
                                     "       if (number == 2)\n" +
                                     "         number += number;\n" +
                                     "       else if (number == 3)\n" +
                                     "         number += number * 2;\n" +
                                     "       else number *= number;\n" +
                                     "       \n" +
                                     "       int number = 2;\n" +
                                     "       switch (number)\n" +
                                     "       {\n" +
                                     "           case 2:\n" +
                                     "               number += number;\n" +
                                     "               break;\n" +
                                     "           case 3:\n" +
                                     "               number += number * 2;\n" +
                                     "               break;\n" +
                                     "           default:\n" +
                                     "               number *= number;\n" +
                                     "               break;\n" +
                                     "       }\n" +
                                     "       Конструкция switch будет в данном примере будет проверять совпадения значения number c одним из предусмотренных случаев (case), и если найдёт, выполнит определённый для набор комнад. Если совпадений не будет,\n" +
                                     "будут выполнены команды по умолчанию (в блоке default).\n" +
                                     "       Как видно, в некоторых ситуациях предпочтительней использовать switch, а не if else, и мы, в свою очередь, рекомендуем это делать.\n" +
                                     "       Эту (последнюю) задачу можно решить и с помощью switсh, и с помощью if else. Выбор за вами!");
        taskStartCodes.Add("public int Execute(int secretSymbol)\n" +
                           "{\n" +
                           "    \n" +
                           "    \n" +
                           "}\n");
    }

    private void FormTasks_Level_3()
    {

    }

    private void FormTasks_Level_4()
    {

    }

    private void FormTasks_Level_5()
    {

    }

    private void Update()
    {
        if (isNextTaskButtonAvailable)
            nextTaskButton.gameObject.SetActive(true);
        else nextTaskButton.gameObject.SetActive(false);
    }

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        currentExtendedTaskTitle = GameObject.Find("TaskTitle_Extended").GetComponent<Text>();
        currentExtendedTaskDescription = GameObject.Find("TaskDescription_Extended").GetComponent<Text>();
    }

    private void Start()
    {   
        startButton = GameObject.Find("StartButton");
        UICollector = GameObject.Find("UI_Collector");
        currentTaskTitle = GameObject.Find("TaskTitle").GetComponent<Text>();
        currentTaskDescription = GameObject.Find("TaskDescription").GetComponent<Text>();
        pad = GameObject.Find("Pad").GetComponent<PadBehaviour>();
        codeField = GameObject.Find("CodeField").GetComponent<InputField>();
        resultField = GameObject.Find("ResultField").GetComponent<InputField>();
        outputField = GameObject.Find("OutputField").GetComponent<InputField>();
        nextTaskButton = GameObject.Find("NextTaskButton").GetComponent<Button>();
        nextLevelButton = GameObject.Find("NextLevelButton").GetComponent<Button>();
        nextTaskButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        isNextTaskButtonAvailable = false;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch(sceneIndex)
        {
            case 1:
                FormTasks_Level_1();
                break;
            case 2:
                FormTasks_Level_2();
                break;
            case 3:
                FormTasks_Level_3();
                break;
            case 4:
                FormTasks_Level_4();
                break;
            case 5:
                FormTasks_Level_5();
                break;
        }      
    }
}
