import { $ } from "bun";
import moment from "moment";
import logger from "pino";

const log = logger();

const timestamp = moment().format("YYYY-MM-DD_hh-mm-ss"); // Return today's date time

log.info('Creating new copy')
// Sync new copy
await $`rsync -a /etc/homeassistant /mnt/usb/backup_homeassistant_${timestamp}`;

log.info('Only keep last 3 copies')
// Only keep last 3 copies
await $`cd /mnt/usb && ls | grep homeassistant | tail -n +4 | xargs rm -rf`;
