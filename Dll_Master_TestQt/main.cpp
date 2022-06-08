#include <QtCore/QCoreApplication>
#include <QDebug>
#include <QLibrary>

typedef int (*FUNC_divide)(int arg);

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);

    QString pathDll = "cdll/Dll_Master.dll";
    QLibrary master(pathDll);
    if (master.load()) {

        qDebug() << "ffffffffff";

        FUNC_divide divide = (FUNC_divide)master.resolve("divide");
        qDebug() << "Hello world "<<divide(19);

        master.unload();
    }
    else {
        qDebug() << "dll load failed";
    }

    return a.exec();
}
