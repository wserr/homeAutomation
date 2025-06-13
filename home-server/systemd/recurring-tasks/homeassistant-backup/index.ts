import { $ } from "bun";
import moment from "moment";
import logger from "pino";
import { parseArgs } from "util";

const { values } = parseArgs({
  args: Bun.argv,
  options: {
    mode: {
      type: "string",
      required: true,
    },
    sourceFolder: {
      type: "string",
      required: true,
    },
    targetFolder: {
      type: "string",
      required: true,
    },
    backupName: {
      type: "string",
      required: true,
    },
  },
  strict: true,
  allowPositionals: true,
});
const log = logger();

switch (values.mode) {
  case "hot":
    await hotCopy(
      values.sourceFolder!,
      values.targetFolder!,
      values.backupName!,
    );
    break;
  case "cold":
    await coldCopy(
      values.sourceFolder!,
      values.targetFolder!,
      values.backupName!,
    );
    break;
}

// 3 cold copies are stored
async function hotCopy(
  sourceFolder: string,
  targetFolder: string,
  backupName: string,
) {
  const timestamp = moment().format("YYYY-MM-DD_HH-mm-ss"); // Return today's date time

  log.info("Creating new copy");
  await $`mkdir -p ${targetFolder}`;
  // Sync new copy
  await $`rsync -a ${sourceFolder} ${targetFolder}/backup_${backupName}_${timestamp}`;

  log.info("Only keep last 3 copies");
  // Only keep last 3 copies
  await $`cd ${targetFolder} && ls | grep ${backupName} | sort -r | tail -n +4 | xargs rm -rf`;
}

// Only 1 cold copy is retained
async function coldCopy(
  sourceFolder: string,
  targetFolder: string,
  backupName: string,
) {
  const timestamp = moment().format("YYYY-MM-DD_HH-mm-ss"); // Return today's date time

  log.info("Creating new copy");

  // find latest backup
  const sourceFolderToBackup =
    await $`cd ${sourceFolder} && ls | grep ${backupName} | sort | tail -n 1`.text().then(resp => resp.replace('\n',''));
  log.info(`Folder to back up: ${sourceFolderToBackup}`);
  await $`mkdir -p ${targetFolder}`;
  await $`zip -r ${targetFolder}/${backupName}_${timestamp}.zip ${sourceFolder}/${sourceFolderToBackup}`;

  log.info("Only keep last copy");
  // Only keep last copy
  await $`cd ${targetFolder} && ls | grep ${backupName} | sort -r | tail -n +2 | xargs rm -rf`;
}
