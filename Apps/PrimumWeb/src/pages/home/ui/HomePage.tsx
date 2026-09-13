import { AboutUsBlock } from '../blocks/about-us/AboutUsBlock';
import { CoursesBlock } from '../blocks/courses-block/CoursesBlock';
import { FAQBlock } from '../blocks/faq-block/FAQBlock';
import { HowToBeginBlock } from '../blocks/how-to-begin-block/HowToBeginBlock';
import { IntegrationBlock } from '../blocks/integration-block/IntegrationBlock';
import styles from './HomePage.module.css'
import { CommonTeacherInfoBlock } from '../blocks/teacher-blocks/common-teacher-info-block/CommonTeacherInfo';
import { GamificationTeacherBlock } from '../blocks/teacher-blocks/gamification-teacher-block/GamificationTeacherBlock';
import { MonetizationTeacherBlock } from '../blocks/teacher-blocks/monetization-teacher-block/MonetizationTeacherBlock';
import { ManagementTeacherBlock } from '../blocks/teacher-blocks/management-teacher-block/ManagementTeacherBlock';
import { IntegrationTeacherBlock } from '../blocks/teacher-blocks/integration-teacher-block/IntegrationTeacherBlock';
import { HowToBeginTeacherBlock } from '../blocks/teacher-blocks/how-to-begin-teacher-block/HowToBeginTeacherBlock';
import { GeneralBlock } from '../blocks/general-block/GeneralBlock';
import TerminalHeroBackground from '../blocks/terminal-hero/TerminalHeroBackground';

export const HomePage = () => {

  return (
    <div className={styles.homePage}>
      <div style={{ position: "relative" }}>
        <TerminalHeroBackground height={'35vh'}/>
        <GeneralBlock />
      </div>
      <div className={styles.blocks}>
        <AboutUsBlock />
        <CoursesBlock />
        <HowToBeginBlock />
        <IntegrationBlock />
        <FAQBlock />
        <CommonTeacherInfoBlock/>
        <ManagementTeacherBlock/>
        <IntegrationTeacherBlock/>
        <GamificationTeacherBlock/>
        <MonetizationTeacherBlock/>
        <HowToBeginTeacherBlock />
      </div>
    </div>
  );
};