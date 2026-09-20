import { useEffect, useRef, useState } from "react";
 
/**
 * TerminalHeroBackground — тот же бегущий терминал, но как фоновый слой.
 *
 * Как это работает:
 * - Обёртка стоит position: absolute; inset: 0, поэтому растягивается
 *   на размер ближайшего родителя с position: relative (или fixed, см. проп fixed)
 *   и не занимает место в потоке документа — остальной контент её "не видит".
 * - pointerEvents: "none" — клики, наведения и выделение текста проходят
 *   насквозь к элементам, лежащим поверх.
 * - zIndex низкий (по умолчанию 0) — любой контент с z-index выше окажется поверх.
 *
 * Важно: у родительского контейнера, куда вы кладёте этот компонент,
 * должен быть position: relative (или он должен сам создавать stacking context),
 * иначе фон растянется на весь ближайший позиционированный элемент выше по дереву.
 *
 * Использование:
 *   <div style={{ position: "relative" }}>
 *     <TerminalHeroBackground />
 *     ...остальной контент страницы поверх...
 *   </div>
 *
 * Если нужно, чтобы терминал был фоном всей страницы (за хедером, футером и т.д.),
 * передайте fixed={true} — тогда он привяжется к viewport, а не к родителю.
 */
 
type TerminalHeroBackgroundProps = {
  lines?: string[];
  typingSpeedMs?: number;
  lineDelayMs?: number;
  restartDelayMs?: number;
  /** Прозрачность текста, чтобы он не спорил с контентом поверх (0..1) */
  opacity?: number;
  /** z-index фонового слоя */
  zIndex?: number;
  /** Привязать к viewport (position: fixed) вместо ближайшего relative-родителя */
  fixed?: boolean;
  /** Высота блока: "100%" (по умолчанию, во весь родитель), px, vh и т.д. */
  height?: string | number;
};
 
const DEFAULT_LINES = [
  "$ npm create primumcode@latest",
  "> Куда движемся сегодня?",
  "$ cd primumcode/core",
  "$ git checkout -b feature/new-idea",
  "$ pip install -r requirements.txt",
  "> Устанавливаю зависимости... готово ✓",
  "$ python train_model.py --epochs=50",
  "> Epoch 12/50 | loss: 0.0842 | acc: 0.97",
  "$ docker build -t primumcode/app .",
  "> Собираю образ... [########--] 82%",
  "$ docker run -p 3000:3000 primumcode/app",
  "> Сервер запущен на порту 3000",
  "$ git add . && git commit -m \"идея стала кодом\"",
  "$ git push origin main",
  "> Ученик подключился к сессии",
  "$ npm run test",
  "> 34 passed, 0 failed (1.2s)",
  "$ ./run.sh --mode=learn",
  "> Компиляция прошла успешно ✓",
  "$ curl -s api.primumcode.dev/status",
  "> { \"status\": \"ok\", \"uptime\": \"99.98%\" }",
  "$ npm run build",
  "> Билд завершён за 4.3s",
  "$ ssh student@primumcode.dev",
  "> Добро пожаловать в PrimumCode",
  "$ echo \"там, где идеи становятся кодом\"",
  "$ git rebase -i HEAD~5",
  "> Интерактивный ребейз: 5 коммитов аккуратно объединены",
  "$ git log --oneline --graph -n 5",
  "> * a1b2c3d (HEAD -> main) feat: добавлена валидация",
  "$ git stash push -m \"черновик новой фичи\"",
  "> Сохранено локальных изменений: 3 файла",
  "$ git branch -D temp-experiment",
  "> Ветка temp-experiment успешно удалена",
  "$ git commit --amend --no-edit",
  "> Последний коммит обновлён без изменения сообщения",
  "$ npx eslint src/ --fix --quiet",
  "> Линтинг завершён: 0 предупреждений, 0 ошибок",
  "$ pytest tests/ -v --cov=core",
  "> Coverage: 98.5% | Все 142 теста прошли успешно ✓",
  "$ npm run type-check",
  "> TypeScript: проверка типов прошла без ошибок за 1.8s",
  "$ jest --watch --coverage",
  "> Ожидание изменений в файлах для повторного запуска...",
  "$ npm run format",
  "> Prettier: отформатировано 45 файлов за 0.8s",
  "$ npx husky install",
  "> Git hooks настроены для автоматической проверки",
  "$ docker-compose up -d db redis cache",
  "> Контейнеры primumcode_db, redis и cache запущены",
  "$ kubectl get pods -n primumcode-prod",
  "> app-7d9f8-xyz  1/1  Running  0  15d",
  "$ terraform plan -out=tfplan",
  "> План выполнен: 1 ресурс будет добавлен, 0 изменено",
  "$ docker system prune -f",
  "> Освобождено 2.4 GB дискового пространства",
  "$ ssh deploy@primumcode.dev \"sudo systemctl restart app\"",
  "> Сервис успешно перезапущен. Downtime: 0.4s",
  "$ make clean build",
  "> Очистка кэша и сборка бинарных файлов завершена",
  "$ python analyze_metrics.py --dataset=latest",
  "> Обработано 50,000 записей за 3.2s",
  "$ jupyter notebook --port=8888 --no-browser",
  "> Сервис запущен. Токен: a1b2c3d4e5f6",
  "$ pip freeze > requirements.prod.txt",
  "> Зависимости зафиксированы для продакшена",
  "$ python -m venv venv && source venv/bin/activate",
  "> Виртуальное окружение активировано (venv)",
  "$ redis-cli ping",
  "> PONG",
  "$ node --trace-warnings server.js",
  "> Приложение готово принимать соединения на 0.0.0.0:8080",
  "$ npm run start:mentor-session",
  "> Загрузка материалов курса \"Архитектура ПО\" завершена",
  "$ git review --approve pr-42",
  "> Код-ревью пройдено: отличная работа с паттернами!",
  "$ echo \"Непрерывное обучение — ключ к мастерству\"",
  "> Записано в DevelopingProgrammers.com/core_values",
  "$ npm run generate:docs",
  "> Документация обновлена: 24 новых метода описано",
  "$ pnpm install --frozen-lockfile",
  "> Зависимости установлены из lock-файла за 1.1s",
  "$ go run main.go",
  "> Go-сервер слушает порт :8080",
  "$ check-reliability --module=core",
  "> Оценка отказоустойчивости: 99.99% (Высокая)",
  "$ npm run audit:fix",
  "> Устранено 3 уязвимости в зависимостях",
  "$ echo \"Минимизация рисков начинается с чистого кода\"",
  "> Принцип зафиксирован в guidelines",
  "$ cat /var/log/app.log | grep \"ERROR\" | wc -l",
  "> 0",
  "$ echo \"Ответственность — это когда код работает, даже если тебя нет рядом\"",
  "> Добавлено в DevelopingProgrammers.com/manifesto",
  "$ curl -I https://api.primumcode.dev/health",
  "> HTTP/2 200 OK | server: nginx/1.24 | x-response-time: 12ms",
  "$ npm run build:prod",
  "> Оптимизация бандла: 420kb -> 115kb (gzip)",
  "$ git checkout main && git pull origin main",
  "> Обновление локальной ветки: получено 15 новых коммитов",
  "$ feed-coffee --level=high",
  "> Заряд энергии восполнен на 100%",
  "$ cat /var/log/system.log | grep \"purr\"",
  "> [INFO] Система работает стабильно. Мурлычет.",
  "$ echo \"Командная работа умножает эффективность кода\"",
  "> Логика подтверждена на практике",
  "$ npm run check:dead-code",
  "> Удалено 120 строк неиспользуемого кода",
  "$ echo \"24 года в коде, и каждый день как первый\"",
  "> Опыт — лучший компилятор",
  "$ echo \"Готов к новым вызовам?\"",
  "> _"
];
 
const ACCENT = "var(--color-secondary-base)";
const TEXT = "var(--color-secondary-base)";
const MAX_HISTORY = 60;
 
export default function TerminalHeroBackground({
  lines = DEFAULT_LINES,
  typingSpeedMs = 12,
  lineDelayMs = 180,
  restartDelayMs = 1000,
  opacity = 0.7,
  zIndex = -1,
  fixed = false,
  height = "100%",
}: TerminalHeroBackgroundProps) {
  const [history, setHistory] = useState<string[]>([]);
  const [current, setCurrent] = useState("");
  const [lineIndex, setLineIndex] = useState(0);
  const [charIndex, setCharIndex] = useState(0);
  const [cursorOn, setCursorOn] = useState(true);
  const scrollRef = useRef<HTMLDivElement>(null);
 
  // блинк курсора
  useEffect(() => {
    const t = setInterval(() => setCursorOn((v) => !v), 500);
    return () => clearInterval(t);
  }, []);
 
  // печать текста
  useEffect(() => {
    if (lineIndex >= lines.length) {
      const t = setTimeout(() => {
        setCurrent("");
        setLineIndex(0);
        setCharIndex(0);
      }, restartDelayMs);
      return () => clearTimeout(t);
    }
 
    const fullLine = lines[lineIndex];
 
    if (charIndex < fullLine.length) {
      const t = setTimeout(() => {
        setCurrent(fullLine.slice(0, charIndex + 1));
        setCharIndex((c) => c + 1);
      }, typingSpeedMs);
      return () => clearTimeout(t);
    }
 
    const t = setTimeout(() => {
      setHistory((h) => {
        const next = [...h, fullLine];
        // не даём истории расти бесконечно — держим только последние N строк
        return next.length > MAX_HISTORY ? next.slice(next.length - MAX_HISTORY) : next;
      });
      setCurrent("");
      setCharIndex(0);
      setLineIndex((i) => i + 1);
    }, lineDelayMs);
    return () => clearTimeout(t);
  }, [charIndex, lineIndex, lines, typingSpeedMs, lineDelayMs, restartDelayMs]);
 
  // автопрокрутка вниз
  useEffect(() => {
    if (scrollRef.current) {
      scrollRef.current.scrollTop = scrollRef.current.scrollHeight;
    }
  }, [history, current]);
 
  return (
    <div
      aria-hidden="true"
      className="terminal-hero-bg"
      style={{
        position: fixed ? "fixed" : "absolute",
        top: 0,
        left: 0,
        right: 0,
        height,
        zIndex,
        pointerEvents: "none",
        overflow: "hidden",
        backgroundColor: "var(--color-black)"
      }}
    >
      <style>{`
        @media (max-width: 768px) {
          .terminal-hero-bg {
            color: transparent
          }
        }
      `}</style>
      <div
        ref={scrollRef}
        style={{
          width: "100%",
          height: "100%",
          background: "transparent",
          overflow: "hidden",
          fontFamily: "'JetBrains Mono', 'Courier New', monospace",
          fontSize: 13,
          lineHeight: 1.35,
          userSelect: "none",
          opacity,
        }}
      >
        {history.map((line, i) => (
          <div key={i} style={{ color: lineColor(line) }}>
            {line}
          </div>
        ))}
        <div style={{ color: lineColor(current || lines[lineIndex] || "") }}>
          {current}
          <span
            style={{
              display: "inline-block",
              width: 7,
              height: 13,
              marginLeft: 2,
              background: ACCENT,
              verticalAlign: "text-bottom",
              opacity: cursorOn ? 1 : 0,
            }}
          />
        </div>
      </div>
    </div>
  );
}
 
function lineColor(line: string) {
  if (line.startsWith("$")) return TEXT;
  if (line.startsWith(">")) return ACCENT;
  return TEXT;
}
 