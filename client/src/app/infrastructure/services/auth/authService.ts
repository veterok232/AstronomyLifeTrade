import { Roles } from "./roles";

const prioritizedRoles = [Roles.staff, Roles.manager, Roles.consumer];

const checkRole = (roleName: string): boolean => contextStore.roleName === roleName;

export const isAuthorizedAsOneOf = (roleNames: string[]): boolean => roleNames?.some(x => checkRole(x));

export const isStaff = (): boolean => checkRole(Roles.staff);

export const isManager = (): boolean => checkRole(Roles.manager);

export const isConsumer = (): boolean => checkRole(Roles.consumer);

export const canAccess = (roleName: string): boolean =>
    prioritizedRoles.indexOf(contextStore.roleName) <= prioritizedRoles.indexOf(roleName);

/* export const canSwitchAssignment = (): boolean => {
    return contextStore.hasMultipleAssignments && !contextStore.originAssignmentId;
};

export const hasPermission = (permission: string): boolean =>
    contextStore.permissions?.some(p => p === permission);

export const hasAnyOfPermissions = (permissions: string[]): boolean =>
    permissions.some(permission => hasPermission(permission));

export const hasAllOfPermissions = (permissions: string[]): boolean =>
    permissions.every(permission => hasPermission(permission)); */