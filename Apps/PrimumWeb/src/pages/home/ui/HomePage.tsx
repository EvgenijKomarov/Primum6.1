import { AboutUsBlock } from '../blocks/about-us/AboutUsBlock';
import { CoursesBlock } from '../blocks/courses-block/CoursesBlock';
import { FAQBlock } from '../blocks/faq-block/FAQBlock';
import { GamificationBlock } from '../blocks/gamification-block/GamificationBlock';
import { HowToBeginBlock } from '../blocks/how-to-begin-block/HowToBeginBlock';
import { IntegrationBlock } from '../blocks/integration-block/IntegrationBlock';
import TypewriterText from '../common-elements/TypewriterText/TypewriterText';
import styles from './HomePage.module.css'

export const HomePage = () => {
  return (
    <div className={styles.homePage}>
      <div className={styles.blocks}>
        <div className={styles.homeHeader}>
          <TypewriterText className={styles.title} text='PrimumCode'/>
          <TypewriterText className={styles.subtitle} text='Там, где идеи становятся кодом'/>
        </div>
        <AboutUsBlock />
        <IntegrationBlock />
        <GamificationBlock />
        <CoursesBlock />
        <HowToBeginBlock />
        <FAQBlock />
      </div>
    </div>
  );
}